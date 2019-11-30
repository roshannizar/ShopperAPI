using ShopperCart.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopperCart.Product
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProduct(int id);
        void Create(ProductDto product);
        void Update(int productId, int quantity);
    }
}
