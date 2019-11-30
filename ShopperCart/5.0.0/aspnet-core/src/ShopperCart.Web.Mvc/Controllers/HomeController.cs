using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ShopperCart.Controllers;

namespace ShopperCart.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ShopperCartControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
