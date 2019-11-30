using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ShopperCart.Authorization;

namespace ShopperCart
{
    [DependsOn(
        typeof(ShopperCartCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ShopperCartApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ShopperCartAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ShopperCartApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
