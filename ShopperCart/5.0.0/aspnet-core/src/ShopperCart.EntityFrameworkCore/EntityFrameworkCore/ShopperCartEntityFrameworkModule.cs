using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using ShopperCart.EntityFrameworkCore.Seed;

namespace ShopperCart.EntityFrameworkCore
{
    [DependsOn(
        typeof(ShopperCartCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class ShopperCartEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<ShopperCartDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        ShopperCartDbContextConfigurer.Configure(options.DbContextOptions, 
                            options.ExistingConnection);
                    }
                    else
                    {
                        ShopperCartDbContextConfigurer.Configure(options.DbContextOptions, 
                            options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ShopperCartEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
