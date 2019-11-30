using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopperCart.Controllers;
using ShopperCart.Customer;
using ShopperCart.Web.Models.Customer;

namespace ShopperCart.Web.Mvc.Controllers
{
    public class CustomerController : ShopperCartControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            var customerDtos = customerService.GetCustomers();
            var model = ObjectMapper.Map<IEnumerable<CustomerViewModel>>(customerDtos);
            return View(model);
        }
    }
}