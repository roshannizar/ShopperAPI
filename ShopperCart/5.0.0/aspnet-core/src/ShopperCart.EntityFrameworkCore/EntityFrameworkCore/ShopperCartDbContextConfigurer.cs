using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ShopperCart.EntityFrameworkCore
{
    public static class ShopperCartDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ShopperCartDbContext> builder, 
            string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ShopperCartDbContext> builder, 
            DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
