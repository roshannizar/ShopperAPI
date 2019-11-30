using ShopperCart.Customers.Dto;
using System.Collections.Generic;
using System.Linq;

namespace ShopperCart.Customer
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetCustomers();
    }
}
