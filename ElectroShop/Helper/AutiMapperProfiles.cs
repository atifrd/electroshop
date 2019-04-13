using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Models;
using ElectroShop.Dtos;

namespace ElectroShop.Helper
{
    public class AutiMapperProfiles : Profile
    {
        public AutiMapperProfiles()
        {
            CreateMap<Category, CategorirsForTreeDto>();
            CreateMap<Property, PropertiesForListDto>();
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<Product, ProductForListDto>();
            
        }
    }
}
