using Microsoft.AspNetCore.Mvc;
using System.Collections;
using th_poker_api.DTO;
using th_poker_api.DTO.Friend;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Room;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.RoomService
{
    public interface IRoomService
    {
        Task <ServiceResponse<List<ListRoomResponse>>> GetAllGames();
        Task<Handling> createRoom(postRoom request);
        Task<Handling> updateRoom(updateRoom request);

        Task <ServiceResponse<List<ListRoomResponse>>> listRoom(listRoom request);
        Task <ServiceResponse<List<GametypeGet>>> gameTypeRoom(roomType request);

        Task<Handling> deleteRoom( GetRoomName request);

        Task<Handling> PlayerJoinRoom(playerRoom request);
    }
}
