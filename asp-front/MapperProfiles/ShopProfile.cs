using asp_front.Models;
using asp_front.Models.ViewModels.Shop;
using ath_server.Models;
using AutoMapper;

namespace asp_front.MapperProfiles;

public class ShopProfile : Profile
{
    public ShopProfile()
    {
        CreateMap<Shop, ShopViewModel>().ReverseMap();
        CreateMap<Shop, EditShopViewModel>()
            .ForMember(
                shop=>shop.ShopOwner,
                opt => 
                    opt.MapFrom(x=>x.OwnerCredentials)
                    )
            .ReverseMap();
    }
}
