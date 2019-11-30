using AutoMapper;
using ShopperCart.Customers.Dto;
using ShopperCart.Order.Dto;
using ShopperCart.Product.Dto;
using ShopperCart.Web.Models.Customer;
using ShopperCart.Web.Models.Order;
using ShopperCart.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopperCart.Web.AutoMapper
{
    public class ViewModelMapper:Profile
    {
        public ViewModelMapper()
        {
            CreateMap<ProductViewModel, ProductDto>().ReverseMap();
            CreateMap<OrderViewModel, OrderDto>().ReverseMap();
            CreateMap<OrderLineViewModel, OrderLineDto>().ReverseMap();
            CreateMap<OrderViewModel, OrderLineViewModel>().ReverseMap();
            CreateMap<CustomerViewModel, CustomerDto>().ReverseMap();
        }
    }
}
