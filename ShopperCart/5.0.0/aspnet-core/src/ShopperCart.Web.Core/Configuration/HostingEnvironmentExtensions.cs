using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ShopperCart.Configuration
{
    public static class HostingEnvironmentExtensions
    {
        [System.Obsolete]
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
