using ShopperCart.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopperCart.Product
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProduct(int id);
        void Create(ProductDto product);
        Task Update(int productId, int quantity);
    }
}
