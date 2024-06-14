namespace th_poker_api.Services.FriendshipService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IFriendshipService
    {
/*        Task<IList<UserServiceModel>> GetFriendsAsync(string userId);
        Task<ICollection<UserServiceModel>> GetNonFriendsAsync(string userId);
        Task<IEnumerable<UserServiceModel>> GetFriendRequestsAsync(string currentUserId);
        Task<IEnumerable<UserServiceModel>> GetPendingRequestsAsync(string currentUserId);
        Task<IEnumerable<UserServiceModel>> GetFriendsByPartNameAsync(string partName, string userId);
*/
        Task<ServiceModelFriendshipStatus> GetFriendshipStatusAsync(string currentUserId, string secondUserId);
        Task SendRequestAsync(string currentUserId, string addresseeId);
        Task AcceptRequestAsync(string currentUserId, string requesterId);
        Task RejectRequestAsync(string currentUserId, string requesterId);
        Task CancelInvitationAsync(string currentUserId, string addresseeId);
        Task UnfriendAsync(string currentUserId, string friendId);
    }
}
