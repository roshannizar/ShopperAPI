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

        [HttpPost]
        public IActionResult CreateOrder([FromBody]OrderViewModel orderViewModel)
        {
            try
            {
                orderViewModel.Status = StatusTypeViewModel.Pending;
                var order = ObjectMapper.Map<OrderDto>(orderViewModel);
                //Create Order
                orderService.CreateOrder(order);

                TempData["Message"] = "Order has been added successfully!";
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error Occured while creating an Order! " + ex;
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult OrderDetail(int id)
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

        [HttpPost]
        public IActionResult OrderDelete(int id)
        {
            try
            {
                orderService.DeleteOrder(id);
                TempData["Message"] = "You have deleted the order Ref No: " + id + " successfully!";
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error occured while deleting the order " + id + "! " + ex;
                throw new Exception();
            }
        }

        [HttpPost]
        public IActionResult OrderEdit([FromBody]List<OrderLineViewModel> orderItemsViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("OrderEdit");

                if (!(orderItemsViewModel.Count > 0))
                    return RedirectToAction("Index");

                var order = ObjectMapper.Map<List<OrderLineDto>>(orderItemsViewModel);
                orderService.UpdateOrder(order);

                TempData["Message"] = "Save changes made for order Ref No: " +
                    orderItemsViewModel[0].OrderId + " successfully!";
                return RedirectToAction("Index", "Order");

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error occured while updating the order! " + ex;
                throw new Exception();
            }
        }
    }
}