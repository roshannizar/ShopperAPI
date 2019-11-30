using System;
using System.Collections.Generic;
using System.Text;

namespace ShopperCart.Order
{
    public class OrderNotFoundException:Exception
    {
        public OrderNotFoundException():base("Order Not Found!")
        {

        }
    }
}
