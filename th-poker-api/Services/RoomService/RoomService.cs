using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Room;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.RoomService
{
    public class RoomService : IRoomService
    {
        #region Function 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private MessageCodes _codes = new MessageCodes();
        public RoomService(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region Create Room
        public async Task<Handling> createRoom(postRoom request)
        {
            var _room = await _context.MDRoomList.Where(u => u.RoomCode.Equals(request.RoomCode) && u.IdStatus.Equals(1)).FirstOrDefaultAsync();

            if (_room != null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Room Ready Exist"
                };
            }

            var mdgames = _context.MDGames.Where(p => p.IdMDGames.Equals(request.IdMDGames)).FirstOrDefault();

            var _postRoom = new MDRoomList
            {
                IdTSRoom = Guid.NewGuid().ToString(),
                RoomCode = request.RoomCode,
                RoomType = request.RoomType,
                MaxPlayer = request.MaxPlayer,
                IdStatus = 1,
                CreatedBy = request.roomMaster,
                CreatedOn = DateTime.Now,
                MDGamesID = mdgames,
                Player = 1
               // IdMdGames = request.IdMDGames
                
            };

           // var roomList = await _context.MDRoomList.Where(u => u.RoomCode.Equals(request.RoomCode)).FirstOrDefaultAsync();
            var _createHeader = new GameplayHeader
            {
                GameplayHeaderID = Guid.NewGuid().ToString(),
                IdTSRoom = _postRoom.IdTSRoom,
                CreatedBy = request.roomMaster,
                CreatedOn = DateTime.Now
            };


            _context.GameplayHeader.Add(_createHeader);
            _context.MDRoomList.Add(_postRoom);
            await _context.SaveChangesAsync();

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Room successfully created!"
            };

        }
        #endregion
        
        #region Update Room
        public async Task<Handling> updateRoom(updateRoom request)
        {

            if(request.IdMDGames == "" || request.IdMDGames == null)
            {
                var roomLis = await _context.MDRoomList.Where(u => u.RoomCode.Equals(request.RoomCode) && u.IdStatus.Equals(1)).FirstOrDefaultAsync();
                if (roomLis == null)
                {
                    return new Handling
                    {
                        Result = false,
                        Code = _codes.notfound,
                        Message = "Room Not Found!"
                    };
                }
                else
                {
                    roomLis.IdStatus = 0;
                    roomLis.UpdatedOn = DateTime.Now;
                    _context.SaveChanges();

                    return new Handling
                    {
                        Result = true,
                        Code = _codes.accepted,
                        Message = "Room Updated"
                    };
                }

            }
            else
            {
                var mdgames = _context.MDGames.Where(p => p.IdMDGames.Equals(request.IdMDGames)).FirstOrDefault();
                var roomList = await _context.MDRoomList.Where(u => u.RoomCode.Equals(request.RoomCode) && u.IdStatus.Equals(1) && u.MDGamesID.Equals(mdgames)).FirstOrDefaultAsync();
                if (roomList == null)
                {
                    return new Handling
                    {
                        Result = false,
                        Code = _codes.notfound,
                        Message = "Room Not Found!"
                    };
                }
                else
                {
                    roomList.IdStatus = 0;
                    roomList.UpdatedOn = DateTime.Now;
                    _context.SaveChanges();

                    return new Handling
                    {
                        Result = true,
                        Code = _codes.accepted,
                        Message = "Room Updated"
                    };
                }
            }
        }
        #endregion

        #region List Room
        public async Task<ServiceResponse<List<ListRoomResponse>>> listRoom(listRoom request)
        {

            var gamesType = await _context.MDGames.Where(u => u.IdMDGames.Equals(request.idMDGames)).FirstOrDefaultAsync();
            var game = await _context.MDRoomList.Where(u => u.IdStatus.Equals(1) && u.MDGamesID.Equals(gamesType) && u.RoomType.Equals(false)).Include(v => v.MDGamesID).ToListAsync();
            ServiceResponse<List<ListRoomResponse>> serviceResponce = new ServiceResponse<List<ListRoomResponse>>();
            
            serviceResponce.data = (game.Select(c => _mapper.Map<ListRoomResponse>(c))).ToList();
            if(serviceResponce.data.Count == 0)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = "Data Empty";
                serviceResponce.data = null;
                return serviceResponce;
            }
            else
                serviceResponce.Success = true;
                serviceResponce.Message = "Success";
                return serviceResponce;
        }
        #endregion

        #region List Game Type
        public async Task<ServiceResponse<List<GametypeGet>>> gameTypeRoom(roomType request)
        {
            var gametype = await _context.MDGames.Where( u => u.isActive.Equals(true)).OrderBy(u => u.StakesMin).ToListAsync();

            ServiceResponse<List<GametypeGet>> serviceResponce = new ServiceResponse<List<GametypeGet>>();
            serviceResponce.data = (gametype.Select(c => _mapper.Map<GametypeGet>(c))).ToList();
            if (serviceResponce.data.Count == 0)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = "Data Empty";
                serviceResponce.data = null;
                return serviceResponce;
            }
            else
                serviceResponce.Success = true;
                serviceResponce.Message = "Success";
                return serviceResponce;
        }
        #endregion

        #region Join Room
        public async Task<Handling> PlayerJoinRoom(playerRoom request)
        {
            var checkRoom = await _context.MDRoomList.Where(u => u.RoomCode.Equals(request.roomCode)).FirstOrDefaultAsync();
            if (checkRoom == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Room Not Found"
                };
            }

            if (checkRoom.Player == 0)
            {
                checkRoom.IdStatus = 0;
                await _context.SaveChangesAsync();
                return new Handling()
                {
                    Result = true,
                    Code = _codes.accepted,
                    Message = "Room Unavailable"
                };
            }

            checkRoom.Player = request.playerNumber;
            await _context.SaveChangesAsync();

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Room List Updated"
            };

        }
        #endregion

        #region New ALl Response Game Type
        public async Task<ServiceResponse<List<ListRoomResponse>>> GetAllGames()
        {
            var game = await _context.MDRoomList.Where(u => u.IdStatus.Equals(1) && u.RoomType.Equals(false) ).Include(v => v.MDGamesID).ToListAsync();
            ServiceResponse<List<ListRoomResponse>> serviceResponce = new ServiceResponse<List<ListRoomResponse>>();
            serviceResponce.data = (game.Select(c => _mapper.Map<ListRoomResponse>(c))).ToList();
            if (serviceResponce.data.Count == 0)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = "Data Empty";
                serviceResponce.data = null;
                return serviceResponce;
            }
            else
                serviceResponce.Success = true;
                serviceResponce.Message = "Success";
                return serviceResponce;
        }
        #endregion

        #region Delete Room

        public async Task<Handling> deleteRoom(GetRoomName request)
        {

            var checkRoom = await _context.MDRoomList.Where(u => u.RoomCode.Equals(request.roomCode)).FirstOrDefaultAsync();
            if (checkRoom == null)
            {
                return new Handling()
                {
                    Result = false,
                    Code = _codes.error,
                    Message = "Room Not Found"
                };
            }


            checkRoom.IdStatus = 0;
            await _context.SaveChangesAsync();

            return new Handling()
            {
                Result = true,
                Code = _codes.accepted,
                Message = "Room Deleted",
            };
        }

      


        #endregion
    }
}
