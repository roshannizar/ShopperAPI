using Abp.Application.Services;
using ShopperCart.Order.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopperCart.Order
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders();
        OrderDto GetOrderById(int id);
        Task CreateOrder(OrderDto orderDto);
        Task UpdateOrder(OrderDto orderDto);
        Task DeleteOrder(int id);
    }
}
