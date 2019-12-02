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
        Task CreateOrder(OrderDto order);
        Task UpdateOrder(List<OrderLineDto> orderLineBOs);
        Task DeleteOrder(int id);
    }
}
