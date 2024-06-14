using AutoMapper;
using System.Security.Cryptography;
using th_poker_api.DTO.History;
using th_poker_api.DTO.Purchase;
using th_poker_api.DTO.Response;
using th_poker_api.Model.Room;
using th_poker_api.Model.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace th_poker_api.Services.HistoryService
{
    public class HistoryService : IHistoryService
    {
        
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();

        private readonly Functions _func = new Functions();

        public HistoryService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region TransferHistory
        public async Task<ServiceResponse<List<TransferHistoryDto>>> GetTransferHistoryReceiver(string userId)
        {
            ServiceResponse<List<TransferHistoryDto>> serviceResponse = new ServiceResponse<List<TransferHistoryDto>>();
            var param = new SqlParameter("@userId", userId);

            var transfers = await(from tc in _context.TSTransferChip
                            join m in _context.MDUsers on tc.Sender equals m.UserId
                            join m2 in _context.MDUsers on tc.Receiver equals m2.UserId
                            //where tc.Sender == userId || 
                            where tc.Receiver == userId
                            select new TransferHistoryDto
                            {
                                TransferID = tc.TransferID,
                                Amount = tc.Amount,
                                Sender = m.UserName,
                                Receiver = m2.UserName,
                                dateTime = tc.CreatedAt.ToString("dd MMM yyyy")
                            }).ToListAsync();

            if (transfers == null || transfers.Count < 1)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Data Empty";
                serviceResponse.data = null;
                return serviceResponse;
            }
            else
            {
                serviceResponse.data = transfers;
                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<TransferHistoryDto>>> GetTransferHistorySender(string userId)
        {
            ServiceResponse<List<TransferHistoryDto>> serviceResponse = new ServiceResponse<List<TransferHistoryDto>>();
            var param = new SqlParameter("@userId", userId);

            var transfers = await (from tc in _context.TSTransferChip
                                   join m in _context.MDUsers on tc.Sender equals m.UserId
                                   join m2 in _context.MDUsers on tc.Receiver equals m2.UserId
                                   where tc.Sender == userId //|| tc.Receiver == userId
                                   select new TransferHistoryDto
                                   {
                                       TransferID = tc.TransferID,
                                       Amount = tc.Amount,
                                       Sender = m.UserName,
                                       Receiver = m2.UserName,
                                       dateTime = tc.CreatedAt.ToString("dd MMM yyyy")
                                   }).ToListAsync();

            if (transfers == null || transfers.Count < 1)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Data Empty";
                serviceResponse.data = null;
                return serviceResponse;
            }
            else
            {
                serviceResponse.data = transfers;
                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
            }
            return serviceResponse;
        }


        #endregion
        #region Top Up
        public async Task<ServiceResponse<List<responseTopupH>>> historyTopup(topupHistoryDto request)
        {
            
            var history = await _context.TSPurchases.Where(u => u.IdUser.Equals(request.UserId)).OrderByDescending(p => p.CreatedOn).Take(50).ToListAsync();

            ServiceResponse<List<responseTopupH>> serviceResponse = new ServiceResponse<List<responseTopupH>>();
            serviceResponse.data = (history.Select(c => _mapper.Map<responseTopupH>(c))).ToList();

            foreach (var tmpData in serviceResponse.data)
            {
                tmpData.DateView = tmpData.Date.ToString("dd MMM yyyy");
                //tmpData.DateView = tmpData.Date.ToString("yyyy-MM-dd HH:mm");
            }

            if (serviceResponse.data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Data Empty";
                serviceResponse.data = null;
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
            }

            return serviceResponse;
        }
        #endregion

        #region history Game
        public async Task<ServiceResponse<List<responseGameH>>> historyGame (topupHistoryDto request, CancellationToken ct)
        {

            var user = await _context.MDUsers
                            .Where(u => u.UserId.Equals(request.UserId))
                            .FirstOrDefaultAsync();

            var history = await _context.GameplayDetail
                .Where(u => u.IdUser.Equals(user))
                .Include(v => v.GMHeaderId)
                .AsNoTracking()
                .OrderByDescending(v => v.CreatedOn)
                .Take(50)
                .ToListAsync();

            List<responseGameH> histories = new List<responseGameH>();
            foreach (var dataHistory in history)
            {
                if(dataHistory.Balance > 0)
                {
                    var historySingle = new responseGameH();
                    var game = await _context.MDRoomList
                        .Where(Q => Q.IdTSRoom.Equals(dataHistory.IDTSRooms))
                        .Select(Q => new { Q.RoomCode, Q.MDGamesID })
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    historySingle.gameplayId = dataHistory.GameplayDetailID;
                    historySingle.balance = dataHistory.Balance;
                    historySingle.date = dataHistory.CreatedOn.Value.ToString("dd MMM yyyy");
                    historySingle.roomCode = game.RoomCode;
                    historySingle.status = dataHistory.Status;
                    historySingle.roomName = game.MDGamesID.GameTitle;
                    historySingle.gameId = game.MDGamesID.IdMDGames;
                    histories.Add(historySingle);
                }
                
            }

            ServiceResponse<List<responseGameH>> serviceResponse = new ServiceResponse<List<responseGameH>>();
            serviceResponse.data = histories.OrderByDescending(q => q.date).ToList();
            if (histories == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Data Empty";
                serviceResponse.data = null;
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Success";
                return serviceResponse;
            }

        }
        #endregion

        #region history Winning Hand
        public async Task<ServiceResponse<List<responseWinningH>>> historyWinning(topupHistoryDto request)
        {
            var user = await _context.MDUsers
                              .Where(u => u.UserId.Equals(request.UserId))
                              .FirstOrDefaultAsync();

            var history = await _context.GameplayDetail
                .Where(u => u.IdUser.Equals(user) && (u.Card1 > 0 && u.Card2 > 0 && u.Card3 > 0 && u.Card4 > 0 && u.Card5 > 0) && u.Status == "0" && u.Balance > 0)
                .Include(v => v.GMHeaderId)
                .Select(u => new responseWinningH
                {
                    Balance = (float)u.Balance,
                    Card1 = (int)u.Card1,
                    Card2 = (int)u.Card2,
                    Card3 = (int)u.Card3,
                    Card4= (int)u.Card4,
                    Card5 = (int)u.Card5,
                    CardUser1= (int)u.CardUser1,
                    CardUser2= (int)u.CardUser2,
                    Date = Convert.ToDateTime(u.CreatedOn).ToString("dd MMM yyyy"),
                    GamePlayid = u.GameplayDetailID,
                    UpdatedOn = u.CreatedOn,
                    Decs = u.WinTypeId.Desc
                })    
                .AsNoTracking()
                .Take(50)
                .OrderByDescending(v => v.Balance)
                .ToListAsync();


            ServiceResponse<List<responseWinningH>> serviceResponse = new ServiceResponse<List<responseWinningH>>();
            serviceResponse.data = history;


            if (serviceResponse.data == null)
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
        #region leaderboard
        public async Task<ServiceResponse<List<leaderboard>>> leaderboard()
        {
            var topUsers = await _context.MDUsers
               .Select(user => new leaderboard
               {
                   UserID = user.UserId,
                   UserName = user.UserName,
                   Chips =
                   (float)(_context.UserAmount.Where(u => u.IdUser == user.UserId).Sum(v => v.amount) +
                           _context.TSPurchases.Where(u => u.IdUser == user.UserId).Sum(v => v.Amount) +
                           _context.GameplayDetail.Where(u => u.IdUser == user && u.Status == "0").Sum(p => p.Balance) +
                           _context.TSJackpotWinners.Where(u => u.CreatedBy.Equals(user.UserId) && u.Claim == true).Sum(p => p.Amount) +
                           _context.TSTransferChip.Where(u => u.Receiver == user.UserId).Sum(p => p.Amount) -
                           _context.tableBets.Where(u => u.createdBy == user.UserId).Sum(p => p.amt) -
                           _context.TSTransferChip.Where(u => u.Sender == user.UserId).Sum(p => p.Amount)
                           ),
                   ProfilePicture = user.profilePicture
               })
           .OrderByDescending(user => user.Chips)
           .Take(50)
           .ToListAsync();


            ServiceResponse<List<leaderboard>> serviceResponse = new ServiceResponse<List<leaderboard>>();
            serviceResponse.data = topUsers;


            if (serviceResponse.data == null | serviceResponse.data.Count == 0)
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

        #region JackpotLeaderboard
        public async Task<ServiceResponse<List<JackpotLeaderboard>>> leaderboardJackpot()
        {
            var topUsers = await _context.MDUsers
               .Select(user => new JackpotLeaderboard
               {
                   UserID = user.UserId,
                   UserName = user.UserName,
                   JP = _context.TSJackpotWinners.Count(x => x.CreatedBy.Equals(user.UserId) && x.Claim == true && x.IsBJP == false),
                   ProfilePicture = user.profilePicture
               })
           .OrderByDescending(user => user.JP)
           .Take(50)
           .ToListAsync();


            ServiceResponse<List<JackpotLeaderboard>> serviceResponse = new ServiceResponse<List<JackpotLeaderboard>>();
            serviceResponse.data = topUsers;


            if (serviceResponse.data == null | serviceResponse.data.Count == 0)
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

        #region BJPLeaderboard
        public async Task<ServiceResponse<List<BigJackpotLeaderboard>>> LeaderboardBjp()
        {
            var topUsers = await _context.MDUsers
               .Select(user => new BigJackpotLeaderboard
               {
                   UserID = user.UserId,
                   UserName = user.UserName,
                   BJP = _context.TSJackpotWinners.Count(x => x.CreatedBy.Equals(user.UserId) && x.Claim == true && x.IsBJP == true),
                   ProfilePicture = user.profilePicture
               })
           .OrderByDescending(user => user.BJP)
           .Take(50)
           .ToListAsync();


            ServiceResponse<List<BigJackpotLeaderboard>> serviceResponse = new ServiceResponse<List<BigJackpotLeaderboard>>();
            serviceResponse.data = topUsers;


            if (serviceResponse.data == null | serviceResponse.data.Count == 0)
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



    }
}
