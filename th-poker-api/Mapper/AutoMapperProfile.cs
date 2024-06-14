using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using th_poker_api.DTO.Friend;
using th_poker_api.DTO.History;
using th_poker_api.DTO.Purchase;
using th_poker_api.DTO.Room;
using th_poker_api.DTO.SpinningWheel;
using th_poker_api.Model.Amount;
using th_poker_api.Model.Friend;
using th_poker_api.Model.Purchase;
using th_poker_api.Model.Room;

namespace th_poker_api.Mapper
{
    public class AutoMapperProfile : Profile
    {
        private Functions _func = new Functions();

        public AutoMapperProfile()
        {

            CreateMap<responseSWDto, UsersAmount>();
            CreateMap<UsersAmount, responseSWDto>();

            CreateMap<invitationResponseDto, MDFriends>();
            CreateMap<MDFriends, invitationResponseDto>()
                .ForMember(x => x.InvitationID, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.userId.UserId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.userId.UserName))
                .ForMember(x => x.amount, opt => opt.MapFrom(src => _func.getAmount(src.userId.UserId)))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.userId.UsersLogins.FirstOrDefault().Status))  // theninclude
                .ForMember(x => x.LastLogin, opt => opt.MapFrom(src => src.userId.UsersLogins.FirstOrDefault().LastLogin.ToString())); // theninclude

            CreateMap<seacrhResponseDto, UsersReferal>();
            CreateMap<UsersReferal, seacrhResponseDto>()
                .ForMember(x => x.userId, opt => opt.MapFrom(src => src.UsersId.UserId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.UsersId.UserName));
            
            CreateMap<MDGames , GametypeGet>(); // Get
            CreateMap<GametypeGet, MDGames>(); // Get

            CreateMap<ListRoomResponse, MDRoomList>(); // Get
            CreateMap<MDRoomList, ListRoomResponse>()
                .ForPath(x => x.roomMaster, opt => opt.MapFrom(src => src.CreatedBy))
                .ForPath(x => x.IdGame, opt => opt.MapFrom(src => src.MDGamesID.IdMDGames))
                .ForPath(x => x.BuyInMax, opt => opt.MapFrom(src => src.MDGamesID.BuyInMax))
                .ForPath(x => x.BuyInMin, opt => opt.MapFrom(src => src.MDGamesID.BuyInMin))
                .ForPath(x => x.StakesMax, opt => opt.MapFrom(src => src.MDGamesID.StakesMax))
                .ForPath(x => x.StakesMin, opt => opt.MapFrom(src => src.MDGamesID.StakesMin));

            CreateMap<purchaseResponse, MDPaymentItem>();
            CreateMap<MDPaymentItem, purchaseResponse>()
                .ForMember(x => x.IdPurchase, opt => opt.MapFrom(src => src.IdPymItem))
                .ForMember(x => x.value, opt => opt.MapFrom(src => src.Value))
                .ForMember(x => x.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(x => x.Desc,  opt => opt.MapFrom(src => src.Desc));

            CreateMap<responseTopupH, TSPurchase>();
            CreateMap<TSPurchase, responseTopupH>()
                .ForMember(x => x.status, opt => opt.MapFrom(src => src.IdStatus))
                .ForMember(x => x.Balance, opt => opt.MapFrom(src => src.Amount))
                .ForMember(x => x.Amount_a, opt => opt.MapFrom(src => src.Amount_a))
                .ForMember(x => x.Date, opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<responseGameH, GameplayDetail>();
            CreateMap<GameplayDetail, responseGameH>()
                .ForMember(x => x.roomName, opt => opt.MapFrom(src => src.GMHeaderId.IdTSRoom))
                .ForMember(x => x.balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(x => x.date, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(x => x.status, opt => opt.MapFrom(src => src.Status));

            CreateMap<responseWinningH, GameplayDetail>();
            CreateMap<GameplayDetail, responseWinningH>()
                .ForMember(x => x.GamePlayid, opt => opt.MapFrom(src => src.GameplayDetailID))
                .ForMember(x => x.Decs, opt => opt.MapFrom(src => src.WinTypeId.Desc))
               /* .ForMember(x => x.Card1, opt => opt.MapFrom(src => src.Card1))
                .ForMember(x => x.Card2, opt => opt.MapFrom(src => src.Card2))
                .ForMember(x => x.Card3, opt => opt.MapFrom(src => src.Card3))
                .ForMember(x => x.Card4, opt => opt.MapFrom(src => src.Card4))
                .ForMember(x => x.Card5, opt => opt.MapFrom(src => src.Card5))*/
                .ForMember(x => x.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(x => x.Date, opt => opt.MapFrom(src => src.CreatedOn));
        }
    }
}
