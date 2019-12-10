using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using ShopperCart.Customers.Dto;

namespace ShopperCart.Customer
{
    public class CustomerService : ShopperCartAppServiceBase, ICustomerService
    {
        private readonly IRepository<Models.Customer> repository;
        private readonly IMapper mapper;

        public CustomerService(IRepository<Models.Customer> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customer = repository.GetAll();

            var query = mapper.Map<IEnumerable<CustomerDto>>(customer);
            return query;
        }
    }
}
