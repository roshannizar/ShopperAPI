using Microsoft.AspNetCore.Antiforgery;
using ShopperCart.Controllers;

namespace ShopperCart.Web.Host.Controllers
{
    public class AntiForgeryController : ShopperCartControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
