using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace ShopperCart.Web.Views
{
    public abstract class ShopperCartRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ShopperCartRazorPage()
        {
            LocalizationSourceName = ShopperCartConsts.LocalizationSourceName;
        }
    }
}
