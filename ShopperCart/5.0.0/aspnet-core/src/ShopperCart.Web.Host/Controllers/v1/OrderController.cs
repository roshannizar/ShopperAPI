using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopperCart.Controllers;
using ShopperCart.Order;
using ShopperCart.Order.Dto;
using ShopperCart.Web.Models.Order;

namespace ShopperCart.Web.Host.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ShopperCartControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<OrderViewModel> Get()
        {
            var orders = orderService.GetOrders();
            return mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }

        // POST: api/v1/Order
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
                return Redirect("https://localhost:62114/Order/Get");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error Occured while creating an Order! " + ex;
                throw ex;
            }
        }

        [HttpPut]
        public IActionResult UpdateOrder([FromBody] OrderViewModel orderViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("OrderEdit");

                if (!(orderViewModel.OrderItems.Count > 0))
                    return RedirectToAction("Index");

                var order = ObjectMapper.Map<OrderDto>(orderViewModel);
                orderService.UpdateOrder(order);

                TempData["Message"] = "Save changes made for order Ref No: " +
                    order.OrderItems[0].OrderId + " successfully!";
                return RedirectToAction("Index", "Order");

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error occured while updating the order! " + ex;
                throw new Exception();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
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
    }
}