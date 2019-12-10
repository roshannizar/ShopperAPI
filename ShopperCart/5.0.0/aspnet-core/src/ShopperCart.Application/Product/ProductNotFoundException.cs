using System;
using System.Collections.Generic;
using System.Text;

namespace ShopperCart.Customer
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException():base("Product Not Found!")
        {

        }
    }
}
