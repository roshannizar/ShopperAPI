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

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        // GET: api/v1/Order
        [HttpGet]
        public JsonResult Get()
        {
            var orders = orderService.GetOrders();

            if(orders != null)
            {
                var query = mapper.Map<IEnumerable<OrderViewModel>>(orders);
                return Json(query);
            } 
            else
            {
                return Json("Order not found!");
            }
        }

        // GET: api/v1/Order/9
        [HttpGet("{id}")]
        public JsonResult ShowOrder(int id)
        {
            try
            {
                //Loads the orders
                var ordersBO = orderService.GetOrderById(id);

                //checks whether the order is null or has value
                if (ordersBO == null)
                {
                    return Json("Order not found!");
                }
                else
                {
                    var model = ObjectMapper.Map<OrderViewModel>(ordersBO);
                    return Json(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "No Order found! " + ex;
                throw ex;
            }
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

        // PUT: api/v1/Order/OrderViewModel
        [HttpPut("{id}")]
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

        // DELETE: api/v1/Order/9
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