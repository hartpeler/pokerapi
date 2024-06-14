using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using th_poker_api.DTO.Game;
using th_poker_api.Model.Game;
using th_poker_api.Model.Room;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.GameplayService
{
    public class GameplayService : IGameplayService
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        private Functions _func = new Functions();
        public GameplayService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region table header 
    /*    public async Task<responseGameHeader> GameplayHeader(postGameHeader request)
        {
            var _GPheader = await _context.GameplayHeader.Where(u => u.IdTSRoom.Equals(request.IdTSRoom) && u.UpdatedOn.Equals(null)).OrderByDescending(v => v.CreatedOn).FirstOrDefaultAsync();

            if(_GPheader == null)
            {
                return new responseGameHeader()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Success,",
                };
            }

            if (_GPheader.Card5 == 0)
            {
                _GPheader.Card1 = request.Card1;
                _GPheader.Card2 = request.Card2;
                _GPheader.Card3 = request.Card3;
                _GPheader.Card4 = request.Card4;
                _GPheader.Card5 = request.Card5;
                _GPheader.UpdatedBy = request.RoomMaster;
                _GPheader.UpdatedOn = DateTime.Now;

                var _postHeader = new GameplayHeader
                {
                    GameplayHeaderID = Guid.NewGuid().ToString(),
                    IdTSRoom = request.IdTSRoom,
                    CreatedBy = request.RoomMaster,
                    CreatedOn = DateTime.Now,

                };
                _context.GameplayHeader.Add(_postHeader);

                await _context.SaveChangesAsync();
                return new responseGameHeader()
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success,",
                    GameHeaderId = _postHeader.GameplayHeaderID,
                };
            }
            else
            {
                var _postHeader = new GameplayHeader
                {
                    GameplayHeaderID = Guid.NewGuid().ToString(),
                    IdTSRoom = request.IdTSRoom,
                    Card1 = request.Card1,
                    Card2 = request.Card2,
                    Card3 = request.Card3,
                    Card4 = request.Card4,
                    Card5 = request.Card5,
                    CreatedBy = request.RoomMaster,
                    CreatedOn = DateTime.Now,

                };
                _context.GameplayHeader.Add(_postHeader);

                var _postHeader1 = new GameplayHeader
                {
                    GameplayHeaderID = Guid.NewGuid().ToString(),
                    IdTSRoom = request.IdTSRoom,
                    CreatedBy = request.RoomMaster,
                    CreatedOn = DateTime.Now,

                };
                _context.GameplayHeader.Add(_postHeader1);

                await _context.SaveChangesAsync();
                return new responseGameHeader()
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Success,",
                    GameHeaderId = _postHeader1.GameplayHeaderID,
                };
            }

        }
    */
        #endregion

        #region Sit Table
        public async Task<responseJoinGameDto> joinGame(joinGameDto request) //Join-Table Service
        {
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.IdUser)).FirstOrDefaultAsync();
            var GPHeader = await _context.GameplayHeader.Where(u => u.GameplayHeaderID.Equals(request.GMHeaderId)).FirstOrDefaultAsync();
            var RoomList = await _context.MDRoomList.Where(u => u.IdTSRoom.Equals(GPHeader.IdTSRoom.ToString()))
                .Include(v => v.MDGamesID)
                .FirstOrDefaultAsync();

            float Totalamount = _func.getAmount(user.UserId.ToString());

            if (RoomList.MDGamesID.BuyInMin > request.Balance || Totalamount < request.Balance)
            {
                return new responseJoinGameDto
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "your chip not enough",
                };
            }

            return new responseJoinGameDto
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Success",
                Balance = request.Balance,
                GmHeaderId = GPHeader.GameplayHeaderID
            };
        }

        #endregion

        #region Standup table
        public async Task<responseStandDto> standTable(standupDto request)
        {
            var WinType = await _context.WinType.Where(u => u.IdWinType.Equals(0)).FirstOrDefaultAsync();
            var roomCode = await _context.GameplayHeader.Where(u => u.IdTSRoom.Equals(request.roomCode)).OrderByDescending(u => u.CreatedOn).FirstOrDefaultAsync();
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();

            if(roomCode == null|| user == null)
            {
                return new responseStandDto
                {
                    Result = false,
                    Code = _codes.error,
                    Message = roomCode == null ? "Room Not Found" : "User Not Found",
                };
            }

            var GPHeader = await _context.GameplayHeader.Where(u => u.GameplayHeaderID.Equals(roomCode.GameplayHeaderID.ToString())).FirstOrDefaultAsync();

            var _postGame = new GameplayDetail
            {
                GameplayDetailID = Guid.NewGuid().ToString(),
                IdUser = user,
                GMHeaderId = GPHeader,
                CardUser1 = request.CardUser1,
                CardUser2 = request.CardUser2,
                /* Card1 = request.Card1,
                 Card2 = request.Card2,
                 Card3 = request.Card3,
                 Card4 = request.Card4,
                 Card5 = request.Card5,*/
                Balance = request.Balance,
                Status = "3",
                WinTypeId = WinType,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
            };

            _context.GameplayDetail.Add(_postGame);
            await _context.SaveChangesAsync();

            return new responseStandDto
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Success",
                Balance = request.Balance,
            };
        }
        #endregion

        #region Win Lose table
        public async Task<Handling> winlose(winLose request)
        {
           
            var WinType = await _context.WinType.Where(u => u.IdWinType.Equals(request.WinTypeId)).FirstOrDefaultAsync();
            var roomCode = await _context.GameplayHeader.Where(u => u.IdTSRoom.Equals(request.IdTSRoom)).OrderByDescending(u => u.CreatedOn).FirstOrDefaultAsync();
            var user = await _context.MDUsers.Where(u => u.UserId.Equals(request.UserId)).FirstOrDefaultAsync();
            var mdRoom = await _context.MDRoomList.Where(u => u.IdTSRoom.Equals(roomCode.IdTSRoom))
                .Include(v => v.MDGamesID)
                .FirstOrDefaultAsync();
            var jackpot = await _context.TSJackpots.Where(x => x.GameID.Equals(mdRoom.MDGamesID.IdMDGames)).FirstOrDefaultAsync();
            var bigJackpot = await _context.TSBigJackpots.Where(x => x.GameID.Equals(mdRoom.MDGamesID.IdMDGames)).FirstOrDefaultAsync();

            if (roomCode == null || user == null)
            {
                return new Handling
                {
                    Result = false,
                    Code = _codes.error,
                    Message = roomCode == null ? "Room Not Found" : "User Not Found",
                };
            }


            var GPHeader = await _context.GameplayHeader.Where(u => u.GameplayHeaderID.Equals(roomCode.GameplayHeaderID.ToString())).FirstOrDefaultAsync();

            var _postGame = new GameplayDetail
            {
                GameplayDetailID = Guid.NewGuid().ToString(),
                IdUser = user,
                GMHeaderId = GPHeader,
                IDTSRooms = request.IdTSRoom,
                CardUser1 = request.CardUser1,
                CardUser2 = request.CardUser2,
                Card1 = request.Card1,
                Card2 = request.Card2,
                Card3 = request.Card3,
                Card4 = request.Card4,
                Card5 = request.Card5,
                Balance = request.Balance,
                Status = request.Status,
                WinTypeId = WinType,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
            };


            TSJackpot jackpotd = new TSJackpot();
            TSBigJackpot bigJackpot1 = new TSBigJackpot();

            if (WinType.IdWinType > 7)
            {

                TSJackpotWinner winner = new TSJackpotWinner();
                winner.JackpotWinnerID = Guid.NewGuid().ToString();
                winner.CreatedBy = request.UserId;
                winner.CreatedAt = DateTime.Now;
                winner.GameplayID = _postGame.GameplayDetailID;
                winner.Claim = false;
                winner.Amount = request.JackpotWin;
                winner.IsBJP = WinType.IdWinType == 10;
                
                if(WinType.IdWinType == 10)
                {
                    TSJackpotWinner bjp = new TSJackpotWinner();
                    bjp.JackpotWinnerID = Guid.NewGuid().ToString();
                    bjp.CreatedBy = request.UserId;
                    bjp.CreatedAt = DateTime.Now;
                    bjp.GameplayID = _postGame.GameplayDetailID;
                    bjp.Claim = false;
                    bjp.Amount = request.BigJackpotWin;
                    bjp.IsBJP = WinType.IdWinType == 10;
                    _context.TSJackpotWinners.Add(bjp);
                    bigJackpot.Pool = bigJackpot.Pool < bjp.Amount ? 0 : bigJackpot.Pool - bjp.Amount;
                }

                jackpot.Amount = jackpot.Amount < winner.Amount ? 0 : jackpot.Amount - winner.Amount;
                _context.TSJackpotWinners.Add(winner);


            }
            //save jackpot data after win.
            if (jackpot != null)
            {
                jackpot.GameID = mdRoom.MDGamesID.IdMDGames; ;
                jackpot.Amount = jackpot.Amount + request.JackpotPool;
                jackpot.UpdatedBy = request.UserId;
                jackpot.UpdatedAt = DateTime.Now;
            }
            else
            {
                jackpotd.JackpotID = Guid.NewGuid().ToString();
                jackpotd.GameID = mdRoom.MDGamesID.IdMDGames;
                jackpotd.Amount = request.JackpotPool;
                jackpotd.UpdatedBy = request.UserId;
                jackpotd.UpdatedAt = DateTime.Now;
                _context.TSJackpots.Add(jackpotd);
            }
            //save big jackpot data after win
            if (bigJackpot != null)
            {
                bigJackpot.GameID = mdRoom.MDGamesID.IdMDGames.ToString();
                bigJackpot.Pool = bigJackpot.Pool + request.BigJackpotPool;
                bigJackpot.UpdatedBy = request.UserId;
                bigJackpot.UpdatedAt = DateTime.Now;
            }
            else
            {
                bigJackpot1.GameID = mdRoom.MDGamesID.IdMDGames;
                bigJackpot1.IDTSBigJackpot = Guid.NewGuid().ToString();
                bigJackpot1.Pool = request.BigJackpotPool;
                bigJackpot1.UpdatedBy = request.UserId;
                bigJackpot1.UpdatedAt = DateTime.Now;
                _context.TSBigJackpots.Add(bigJackpot1);
            }

            //save house money
            TSHouse houses = new TSHouse();
            houses.HouseID = Guid.NewGuid().ToString();
            houses.Amount = request.Tax;
            houses.GameplayID = _postGame.GameplayDetailID;
            houses.CreatedAt = DateTime.Now;
            houses.CreatedBy = request.UserId;

            _context.TSHouses.Add(houses);
            _context.GameplayDetail.Add(_postGame);

            await _context.SaveChangesAsync();

            return new Handling
            {
                Result = true,
                Code = _codes.ok,
                Message = "Success",
                Amount1 = bigJackpot == null ? bigJackpot1.Pool : bigJackpot.Pool,
                Amount2 = jackpot == null ? jackpotd.Amount : jackpot.Amount
            };
        }

        #endregion

        #region put bet
        public async Task<ResponseGameDTO> BetAndPut(BetPutDTO request)
        {
            ResponseGameDTO dt = new ResponseGameDTO();
            try
            {
                TableBet tb = new TableBet();
                tb.amt = request.amount;
                tb.createdOn = DateTime.UtcNow;
                tb.idTSRoom = request.idTSRoom;
                tb.createdBy = request.userID;
                tb.idTableBet = Guid.NewGuid().ToString();
                await _context.tableBets.AddAsync(tb);
                _context.SaveChanges();
                dt.code = _codes.ok.ToString();
                dt.message = "Data saved";
            }
            catch(Exception err)
            {
                dt.code = _codes.serverError.ToString();
                dt.message = err.Message.ToString();
            }
            finally
            {
                dt.code = _codes.ok.ToString();
                dt.message = "Update succeeded";
            }
            return dt;
        }
        #endregion
        #region Get Room Detail
        public async Task<Room_Dto> roomDetail(Room_Detail request)
        {
            var roomInfo = await (
                from game in _context.MDGames
                join rooms in _context.MDRoomList on game.IdMDGames equals rooms.MDGamesID.IdMDGames
                join gh in _context.GameplayHeader on rooms.IdTSRoom equals gh.IdTSRoom
                where
                    rooms.RoomCode.Equals(request.RoomCode) && 
                    rooms.IdStatus.Equals(1) &&
                    game.isActive.Equals(true)
                select new Room_Dto
                    {
                        Result = true,
                        IdTSRoom = rooms.IdTSRoom,
                        MdIdGames = game.IdMDGames,
                        gmHeaderId = gh.GameplayHeaderID,
                        gameDesc = game.GameDesc,
                        BuyInMax = game.BuyInMax,
                        BuyInMin = game.BuyInMin,
                        StakesMax = game.StakesMax,
                        StakesMin = game.StakesMin,
                        createdOn = rooms.CreatedOn.ToString("yyyy-MM-dd HH-mm "),
                        totalBJP = _func.totalBJP(rooms.MDGamesID.IdMDGames),
                        totalJP = _func.totalJP(rooms.MDGamesID.IdMDGames)
                    }
                )
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if(roomInfo == null)
            {
                return new Room_Dto
                {
                    Result = false,
                    IdTSRoom = null,
                    gameDesc = null,
                    BuyInMax = null,
                    BuyInMin = null,
                    StakesMax = null,
                    StakesMin = null,
                    createdOn = null
                };
            }
            return roomInfo;  
        }
        #endregion
        #region Transfer
        public async Task<ResponseGameDTO> Transfer(Transfer request)
        {
            ResponseGameDTO dt = new ResponseGameDTO();
            try
            {
                TSTransferChip tb = new TSTransferChip();
                tb.TransferID = Guid.NewGuid().ToString();
                tb.Amount = request.Amount;
                tb.CreatedAt = DateTime.UtcNow;
                tb.CreatedBy = request.UserID;
                tb.Sender = request.UserID;
                tb.Receiver = request.Receiver;
                await _context.TSTransferChip.AddAsync(tb);
                _context.SaveChanges();
                dt.code = _codes.ok.ToString();
                dt.message = "Data saved";
            }
            catch (Exception err)
            {
                dt.code = _codes.serverError.ToString();
                dt.message = err.Message.ToString();
            }
            finally
            {
                dt.code = _codes.ok.ToString();
                dt.message = "Update succeeded";
            }
            return dt;
        }
        #endregion

        #region Update Claim Status
        public async Task<Handling> UpdateClaimStatus(string claimID)
        {
            var data = await _context.TSJackpotWinners.FirstOrDefaultAsync(x => x.JackpotWinnerID.Equals(claimID) && x.Claim == false);

            if (data == null)
            {

                return new Handling
                {
                    Result = false,
                    Code = _codes.notfound,
                    Message = "Jackpot Voucher has been claimed."
                };
            }

            data.Claim = true;

            await _context.SaveChangesAsync();
            return new Handling
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Jackpot has been updated."
            };
        }
        #endregion


        #region Update ClaimList
        public async Task<List<ClaimDTO>> GetClaimList(string UserID)
        {

            var jackpotWinners = await (from a in _context.TSJackpotWinners
                                        join b in _context.GameplayDetail on a.GameplayID equals b.GameplayDetailID
                                        join c in _context.WinType on b.WinTypeId.IdWinType equals c.IdWinType
                                        where a.CreatedBy == UserID && a.Claim == false
                                        select new ClaimDTO
                                        {
                                            ID = a.JackpotWinnerID,
                                            WinType = c.Desc,
                                            Amount = a.Amount,
                                            IsBJP = a.IsBJP
                                        }).ToListAsync();
            return jackpotWinners;
        }
        #endregion
    }
}
