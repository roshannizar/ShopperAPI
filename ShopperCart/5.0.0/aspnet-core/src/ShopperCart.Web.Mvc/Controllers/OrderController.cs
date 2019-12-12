using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopperCart.Controllers;
using ShopperCart.Customer;
using ShopperCart.Order;
using ShopperCart.Order.Dto;
using ShopperCart.Product;
using ShopperCart.Web.Models.Order;

namespace ShopperCart.Web.Mvc.Controllers
{
    public class OrderController : ShopperCartControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly IProductService productService;

        public OrderController(IOrderService orderService, ICustomerService customerService,IProductService productService)
        {
            this.orderService = orderService;
            this.customerService = customerService;
            this.productService = productService;
        }
        public IActionResult Index()
        {
            var OrdersDto = orderService.GetOrders();
            var model = ObjectMapper.Map<IEnumerable<OrderViewModel>>(OrdersDto);
            return View(model);
        }

        public IActionResult OrderItems()
        {
            //Loading product to drop down
            ViewBag.ProductList = productService.GetProducts().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).OrderBy(x => x.Text).ToList();

            //Loading customers to drop down
            ViewBag.CustomerList = customerService.GetCustomers().Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.Id.ToString()
            }).OrderBy(x => x.Text).ToList();

            return View();
        }

        [HttpGet]
        public IActionResult ShowOrder(int id)
        {
            try
            {
                //Loads the orders
                var ordersBO = orderService.GetOrderById(id);

                //checks whether the order is null or has value
                if (ordersBO == null)
                {
                    return NotFound();
                }
                else
                {
                    var model = ObjectMapper.Map<OrderViewModel>(ordersBO);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "No Order found! " + ex;
                throw ex;
            }
        }
    }
}