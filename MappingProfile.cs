using AutoMapper;
using ShopsAPI.Dtos;
using ShopsAPI.Models;

namespace ShopsAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shop, ShopDto>().ReverseMap();
            CreateMap<Shop, GetShopDto>().ReverseMap();
            CreateMap<CreateShopDto, Shop>();
            CreateMap<GetShopItemDto, ShopItem>().ReverseMap();
            CreateMap<ShopItemDto, ShopItem>().ReverseMap();
            CreateMap<CreateShopItemDto, ShopItem>();
        }
    }
}
