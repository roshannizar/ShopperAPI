using AutoMapper;
using ShopperCart.Customers.Dto;
using ShopperCart.Product.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopperCart.Order.Dto
{
    public class OrderDtoMapper:Profile
    {
        public OrderDtoMapper()
        {
            CreateMap<OrderDto, Models.Order>().ReverseMap();
            CreateMap<OrderLineDto, Models.OrderLine>().ReverseMap();
            CreateMap<OrderDto, OrderLineDto>().ReverseMap();
            CreateMap<Models.Order, Models.OrderLine>().ReverseMap();
        }
    }
}
