using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ShopperCart.Configuration;

namespace ShopperCart.Web.Host.Startup
{
    [DependsOn(
       typeof(ShopperCartWebCoreModule))]
    public class ShopperCartWebHostModule: AbpModule
    {
        [System.Obsolete]
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        [System.Obsolete]
        public ShopperCartWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ShopperCartWebHostModule).GetAssembly());
        }
    }
}
