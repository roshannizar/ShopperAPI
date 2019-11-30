using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopperCart.Product.Dto
{
    public class ProductDtoMapper:Profile
    {
        public ProductDtoMapper()
        {
            CreateMap<ProductDto, Models.Product>().ReverseMap();
        }
    }
}
