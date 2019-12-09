using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopperCart.Controllers;
using ShopperCart.Customer;
using ShopperCart.Order;
using ShopperCart.Order.Dto;
using ShopperCart.Product;
using ShopperCart.Web.Models.Order;

namespace Shopper.Web.Host.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ShopperCartControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService,IProductService productService,ICustomerService customerService
            ,IMapper mapper)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
            this.mapper = mapper;
        }

        // GET: api/v1/order
        [HttpGet]
        public IEnumerable<OrderViewModel> Get()
        {
            var query = orderService.GetOrders();

            var model = mapper.Map<IEnumerable<OrderViewModel>>(query);
            return model;
        }

        // GET: api/v1/order/9
        [HttpGet("{id}")]
        public OrderViewModel Get(int id)
        {
            var query = orderService.GetOrderById(id);
            var model = mapper.Map<OrderViewModel>(query);
            return model;
        }

        // POST: api/v1/Order
        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> Post([FromBody] OrderViewModel orderViewModel)
        {
            if(orderViewModel != null)
            {
                var order = mapper.Map<OrderDto>(orderViewModel);
                await orderService.CreateOrder(order);

                return Ok("Order Created Successfully!");
            }
            else
            {
                return BadRequest("Field's were not matching, you are missing something!");
            }
        }

        // PUT: api/v1/Order/9
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderViewModel>> Put([FromBody]OrderViewModel orderViewModel)
        {
            if(orderViewModel == null)
            {
                return BadRequest("Order is not present");
            }

            try
            {
                var order = mapper.Map<OrderDto>(orderViewModel);
                await orderService.UpdateOrder(order);
                return Ok("Order Update Successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //DELETE: api/v1/Order/9
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderViewModel>> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest("Id is not present");
            }

            try
            {
                await orderService.DeleteOrder(id);
                return Ok("Order deleted successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}