using Abp.AspNetCore.Mvc.ViewComponents;

namespace ShopperCart.Web.Views
{
    public abstract class ShopperCartViewComponent : AbpViewComponent
    {
        protected ShopperCartViewComponent()
        {
            LocalizationSourceName = ShopperCartConsts.LocalizationSourceName;
        }
    }
}
