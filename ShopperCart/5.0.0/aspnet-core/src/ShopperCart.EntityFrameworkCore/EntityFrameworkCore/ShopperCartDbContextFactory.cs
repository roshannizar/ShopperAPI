using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShopperCart.Configuration;
using ShopperCart.Web;

namespace ShopperCart.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ShopperCartDbContextFactory : IDesignTimeDbContextFactory<ShopperCartDbContext>
    {
        public ShopperCartDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ShopperCartDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ShopperCartDbContextConfigurer.Configure(builder,
                configuration.GetConnectionString(ShopperCartConsts.ConnectionStringName));

            return new ShopperCartDbContext(builder.Options);
        }
    }
}
