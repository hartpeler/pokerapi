using AutoMapper;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using th_poker_api.DTO.Friend;
using th_poker_api.DTO.History;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Amount;
using th_poker_api.Model.Friend;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.FriendsService
{
    public class FriendsService : IFriendsService
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        private Functions _func = new Functions();
        public FriendsService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region Invitations Friends
        public async Task<ServiceResponse<List<invitationResponseDto>>> invitationList(invitationGetDto request, CancellationToken ct)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
            var userFriends = await _context.MDFriends.Where(u => u.FriendId.Equals(request.UserId)).FirstOrDefaultAsync();
           
            var invitation = await _context.MDFriends.Where(u => u.FriendId.Equals(user.UserId) && u.Status.Equals(request.invitation.ToString()))
                .Include(v => v.userId)
                .Include(v => v.userId.UsersLogins).ToListAsync();

            ServiceResponse<List<invitationResponseDto>> serviceResponse = new ServiceResponse<List<invitationResponseDto>>();
          
     

            invitation = invitation.ToList();//.Where(p => p.userCreateID != user.UserId).ToList();
            serviceResponse.data = (invitation.Select(c => _mapper.Map<invitationResponseDto>(c))).ToList();
           // serviceResponse.data = data;
                if (serviceResponse.data.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Data Empty";
                    serviceResponse.data = null;
                    return serviceResponse;
                }
                else
                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
                return serviceResponse;
            
        }

        #endregion

        #region Friends-Connection
        public async Task<friendsConnectionResponse> friendConnection(friendsConnectionDTo request)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.userId)).FirstOrDefaultAsync();
            var MdFr = await _context.MDFriends.Where(u => u.userId.UserId.Equals(user.UserId) && u.FriendId.Equals(request.friendId)).FirstOrDefaultAsync();

            var fUser = await _context.MDUsers.Where(u => u.UserId.Equals(request.friendId)).FirstOrDefaultAsync();
            var friend = await _context.MDFriends.Where(u => u.userId.UserId.Equals(fUser.UserId) && u.FriendId.Equals(request.userId)).FirstOrDefaultAsync();

            if (user == null || fUser == null )
            {
                return new friendsConnectionResponse
                {
                    results = false
                };
            }

            else
            {
                return new friendsConnectionResponse
                {
                   results = true,
                   invitationId = MdFr.Id,
                   invitationId2 = friend.Id,
                   username = user.UserName,
                   friendName = fUser.UserName
                };
            }

        }
        #endregion

        #region Accept
        public async Task<Handling> acception(acceptDto request)
        {

            var invitation = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID)).Include(v => v.userId).FirstOrDefaultAsync();
           
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(invitation.FriendId)).FirstOrDefaultAsync();


            //var checkInv = await _context.MDFriends.Where(u => u.Status.Equals(1) && u.FriendId.Equals(invitation.userId.UserId)).FirstOrDefaultAsync();

            if (invitation == null)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Error Accept Friends"
                };
            }
            if (invitation.Status == "0")
            {

                var _invitation = new MDFriends
                {
                    Id = Guid.NewGuid().ToString(),
                    userId = user,
                    FriendId = invitation.userCreateID,
                    Status = 1.ToString(),
                    CreatedOn = DateTime.Now,
                    userCreateID = invitation.userCreateID
                };
                _context.MDFriends.Add(_invitation);
                invitation.Status = request.invitation.ToString();
                invitation.UpdatedOn = DateTime.Now;

                await _context.SaveChangesAsync();


            }

            //var friends = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID) && u.Status.Equals(0)).FirstOrDefaultAsync();



            return new Handling
            {

                Result = true,
                Code = _codes.accepted,
                Message = request.invitation == 1 ? "Accepted" : "Decline"
            };


        }

        #endregion

        #region invitation
        public async Task<Handling> invitationFriend(invitationFriendDto request)
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
        
            var mdFr = await _context.MDFriends.Where(u => u.userId.Equals(user)).FirstOrDefaultAsync();

            if (mdFr == null || mdFr == null)
            {

                if (request.UserId == request.FriendId)
                {
                    return new Handling
                    {
                        Result = true,
                        Code = _codes.accepted,
                        Message = "You Cannot Invite Yourself"
                    };
                }

                var _invitation = new MDFriends
                {
                    Id = Guid.NewGuid().ToString(),
                    userId = user,
                    FriendId = request.FriendId,
                    Status = 0.ToString(),
                    CreatedOn = DateTime.Now,
                    userCreateID = request.UserId
                };


                _context.MDFriends.Add(_invitation);
                await _context.SaveChangesAsync();

                return new Handling
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success"
                };

            }


            else if (user.UserId == request.UserId && mdFr.FriendId == request.FriendId && mdFr.Status == "1")
            {
                return new Handling
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "You Have Already Been Friends"
                };
            }

            else if (user.UserId == request.UserId && mdFr.FriendId == request.FriendId && mdFr.Status == "0")
            {
                return new Handling
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Your Invitations has been sent"
                };
            }

            else
            {
                var _invitation = new MDFriends
                {
                    Id = Guid.NewGuid().ToString(),
                    userId = user,
                    FriendId = request.FriendId,
                    Status = 0.ToString(),
                    CreatedOn = DateTime.Now,
                    userCreateID = request.UserId
                };

                _context.MDFriends.Add(_invitation);
                await _context.SaveChangesAsync();

                return new Handling
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success"
                };
            }

        }
        #endregion

        #region seacrh
        public async Task<seacrhResponseDto> searchFriend (searchDto request)
        {
            var search = await _context.UsersReferal.Where(u => u.ReferalCode.Equals(request.referalCode)).Include(v => v.UsersId).FirstOrDefaultAsync();

            if (search == null)
            {
                return new seacrhResponseDto
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "User Not Found"
                };
            }
            else
            {
                return new seacrhResponseDto
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success",
                    userId = search.UsersId.UserId,
                    Name = search.UsersId.UserName,
                    amount = _func.getAmount(search.UsersId.UserId.ToString()),

                };
            }
        }
        /* public async Task<ServiceResponse<List<seacrhResponseDto>>> searchFriend (searchDto request)
         {
             var search = await _context.UsersReferal.Where(u => u.ReferalCode.Contains(request.referalCode)).Include(v => v.UsersId).ThenInclude(v => v.UsersAmounts).ToListAsync();

             ServiceResponse<List<seacrhResponseDto>> serviceResponse = new ServiceResponse<List<seacrhResponseDto>>();
             serviceResponse.data = search.Select(c => _mapper.Map<seacrhResponseDto>(c)).ToList();
             if(serviceResponse.data.Count == 0)
             {
                 serviceResponse.Success = false;
                 serviceResponse.Message = "User Not Found";
                 serviceResponse.data = null;
                 return serviceResponse;
             }
             else
                 serviceResponse.Success = true;
                 serviceResponse.Message = "Success";
                 return serviceResponse;
         }*/
        #endregion

        #region Delete Friend
        public async Task<Handling> delateFriend(delateFriendDto request)
        {
            var friend = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID)).Include(v => v.userId).FirstOrDefaultAsync();
            var friend2 = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID2)).Include(v => v.userId).FirstOrDefaultAsync();
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(friend.FriendId)).FirstOrDefaultAsync();

            if (user == null || friend == null || friend2 == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Error Delete Friend",
                };
            }
            var delete = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID)).FirstOrDefaultAsync();
            var delete2 = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID2)).FirstOrDefaultAsync();

            _context.Remove(delete);
            _context.Remove(delete2);
            await _context.SaveChangesAsync();

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Friend Deleted",
            };
        }
        #endregion

        #region Decline Friend
        public async Task<Handling> declineFriend (declineFriend request)
        {
            var decline = await _context.MDFriends.Where(u => u.Id.Equals(request.InvitationID)).FirstOrDefaultAsync();
            if(decline == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Error Decline Friend",
                };
            }
            _context.Remove(decline);
            await _context.SaveChangesAsync();

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Friend Decline",
            };
        }

        #endregion

    }
}


