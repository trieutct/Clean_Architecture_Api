using AutoMapper;
using Clean_Architecture.Model.Dto.AccountClient;
using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Model.Dto.FavoriteProduct;
using Clean_Architecture.Model.Dto.Product;
using Clean_Architecture.Model.Dto.Cart;
using Clean_Architecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AccountClient, AccountClientDto>().ReverseMap();
            CreateMap<FavoriteProduct, FavoriteProductDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
        }

    }
}
