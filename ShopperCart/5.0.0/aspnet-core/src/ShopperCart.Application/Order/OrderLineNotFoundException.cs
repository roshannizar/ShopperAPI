using System;
using System.Collections.Generic;
using System.Text;

namespace ShopperCart.Order
{
    public class OrderLineNotFoundException:Exception
    {
        public OrderLineNotFoundException():base("Order Item not found!")
        {

        }
    }
}
