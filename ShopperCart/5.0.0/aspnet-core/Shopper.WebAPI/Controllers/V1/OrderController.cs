using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopperCart.Customer;
using ShopperCart.Order;
using ShopperCart.Order.Dto;
using ShopperCart.Product;
using ShopperCart.Web.Models.Order;

namespace Shopper.WebAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
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
    }
}