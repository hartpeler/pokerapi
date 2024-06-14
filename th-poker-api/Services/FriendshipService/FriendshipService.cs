using AutoMapper;
using th_poker_api.Model.Friend;

namespace th_poker_api.Services.FriendshipService
{
    public class FriendshipService : IFriendshipService
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        public FriendshipService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region Get Status Friend
        public async Task<ServiceModelFriendshipStatus> GetFriendshipStatusAsync(string currentUserId, string userId)
        {
            var friendship = await _context
                .MDFriendship
                    .FirstOrDefaultAsync(u =>
                        u.AddresseeId == currentUserId && u.RequesterId == userId ||
                        u.AddresseeId == userId && u.RequesterId == currentUserId);

            if (friendship == null)
            {
                return ServiceModelFriendshipStatus.NonFriends;
            }

            switch (friendship.Status)
            {
                case Status.Accepted:
                    return ServiceModelFriendshipStatus.Accepted;
                case Status.Pending:
                    return ServiceModelFriendshipStatus.Pending;
            }
            return ServiceModelFriendshipStatus.Request;
        }
        #endregion

        #region SendRequestAsync
        public async Task SendRequestAsync(string currentUserId, string addresseeId)
        {
            if (!await IsFriendshipExistAsync(currentUserId, addresseeId))
            {
                var friendship = new MDFriendship()
                {
                    AddresseeId = addresseeId,
                    RequesterId = currentUserId,
                    Status = Status.Pending
                };

                await _context.MDFriendship.AddAsync(friendship);
                await _context.SaveChangesAsync();
            }
        }
        #endregion


        #region Accept Friends
        public async Task AcceptRequestAsync(string currentUserId, string requesterId)
        {
            if (await IsFriendshipExistAsync(currentUserId, requesterId))
            {
                var friendship = await GetFriendshipAsync(requesterId, currentUserId);

                if (friendship.Status == Status.Pending)
                {
                    friendship.Status = Status.Accepted;

                    await _context.SaveChangesAsync();
                }
            }
        }
        #endregion

        #region Reject Friends
        public async Task RejectRequestAsync(string currentUserId, string requesterId)
        {
            if (await IsFriendshipExistAsync(currentUserId, requesterId))
            {
                var friendship = await GetFriendshipAsync(requesterId, currentUserId);

                await RemoveFriendshipAsync(friendship);
            }
        }
        #endregion

        #region Cancel Friends
        public async Task CancelInvitationAsync(string currentUserId, string addresseeId)
        {
            if (await IsFriendshipExistAsync(currentUserId, addresseeId))
            {
                var friendship = await GetFriendshipAsync(currentUserId, addresseeId);

                await RemoveFriendshipAsync(friendship);
            }
        }
        #endregion

        #region Unfriend
        public async Task UnfriendAsync(string currentUserId, string friendId)
        {
            if (await IsFriendshipExistAsync(currentUserId, friendId))
            {
                var friendship = await GetFriendshipAsync(currentUserId, friendId) == null ?
                    await GetFriendshipAsync(friendId, currentUserId) :
                    await GetFriendshipAsync(currentUserId, friendId);

                await RemoveFriendshipAsync(friendship);
            }
        }
        #endregion 

        #region Private Method
        private async Task<bool> IsFriendshipExistAsync(string currentUserId, string addresseeId)
        => await _context
            .MDFriendship
                .AnyAsync(u => u.RequesterId == currentUserId &&
                            u.AddresseeId == addresseeId ||
                            u.RequesterId == addresseeId &&
                            u.AddresseeId == currentUserId);

        private async Task<MDFriendship> GetFriendshipAsync(string requesterId, string addresseeId)
        => await _context
            .MDFriendship
                .FirstOrDefaultAsync(i => i.RequesterId == requesterId &&
                                        i.AddresseeId == addresseeId);

        private async Task RemoveFriendshipAsync(MDFriendship friendship)
        {
            if (friendship != null)
            {
                _context.MDFriendship.Remove(friendship);

                await _context.SaveChangesAsync();
            }
        }

        #endregion

    }
}
