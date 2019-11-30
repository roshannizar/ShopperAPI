using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopperCart.Controllers;
using ShopperCart.Product;
using ShopperCart.Web.Models.Product;

namespace ShopperCart.Web.Mvc.Controllers
{
    public class ProductController : ShopperCartControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public ActionResult Index()
        {
            var productDto = productService.GetProducts();
            var model = ObjectMapper.Map<IEnumerable<ProductViewModel>>(productDto);
            return View(model);
        }

        public JsonResult GetProductById(int id)
        {
            var productDto = productService.GetProduct(id);
            var product = ObjectMapper.Map<ProductViewModel>(productDto);
            return Json(product);
        }
    }
}