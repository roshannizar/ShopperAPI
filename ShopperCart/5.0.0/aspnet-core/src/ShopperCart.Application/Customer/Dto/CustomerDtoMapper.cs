using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopperCart.Customers.Dto
{
    public class CustomerDtoMapper:Profile
    {
        public CustomerDtoMapper()
        {
            CreateMap<CustomerDto, Models.Customer>().ReverseMap();
        }
    }
}
