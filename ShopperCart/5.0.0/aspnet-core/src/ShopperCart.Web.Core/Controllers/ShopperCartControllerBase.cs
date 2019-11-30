using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ShopperCart.Controllers
{
    public abstract class ShopperCartControllerBase: AbpController
    {
        protected ShopperCartControllerBase()
        {
            LocalizationSourceName = ShopperCartConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
