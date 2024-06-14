using Microsoft.AspNetCore.Mvc;
using th_poker_api.Services.FriendshipService;

namespace th_poker_api.Controllers
{
    public class FriendshipController : Controller
    {
        #region Function
        private readonly IFriendshipService _friendshipService;
        private Functions _func = new Functions();
        private MessageCodes _codes = new MessageCodes();

        public FriendshipController(IFriendshipService friendshipService, DataContext dataContext)
        {
            _friendshipService = friendshipService;
        }
        #endregion
    }
}
