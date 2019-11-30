using ShopperCart.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopperCart.Web.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public StatusTypeViewModel Status { get; set; }
        public List<OrderLineViewModel> OrderItems { get; set; }
        public CustomerViewModel Customers { get; set; }
    }
}
