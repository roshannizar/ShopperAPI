using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ShopperCart.Configuration;
using Abp.AutoMapper;
using ShopperCart.Web.Models.Product;
using ShopperCart.Product.Dto;

namespace ShopperCart.Web.Startup
{
    [DependsOn(typeof(ShopperCartWebCoreModule))]
    public class ShopperCartWebMvcModule : AbpModule
    {
        [System.Obsolete]
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        [System.Obsolete]
        public ShopperCartWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<ShopperCartNavigationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ShopperCartWebMvcModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
