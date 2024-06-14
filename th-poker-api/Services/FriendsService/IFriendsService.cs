using th_poker_api.DTO.Friend;
using th_poker_api.DTO.Response;
using th_poker_api.DTO.Room;
using th_poker_api.Model.Success;

namespace th_poker_api.Services.FriendsService
{
    public interface IFriendsService
    {
        Task<ServiceResponse<List<invitationResponseDto>>> invitationList(invitationGetDto request, CancellationToken ct);
        Task<Handling> acception(acceptDto request);
        Task<Handling> invitationFriend(invitationFriendDto request);
        Task<friendsConnectionResponse> friendConnection(friendsConnectionDTo request);
        Task<seacrhResponseDto> searchFriend(searchDto request);
        Task<Handling> delateFriend(delateFriendDto request);
        Task<Handling> declineFriend(declineFriend request);
       // Task<ServiceResponse<List<seacrhResponseDto>>> searchFriend(searchDto request);
    }
}
