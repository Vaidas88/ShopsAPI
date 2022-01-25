using AutoMapper;
using ShopsAPI.Dtos;
using ShopsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shop, ShopDto>().ReverseMap();
            CreateMap<CreateShopDto, Shop>();
        }
    }
}
