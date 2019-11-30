using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ShopperCart.Authorization.Roles;
using ShopperCart.Authorization.Users;
using ShopperCart.MultiTenancy;
using ShopperCart.Models;

namespace ShopperCart.EntityFrameworkCore
{
    public class ShopperCartDbContext : AbpZeroDbContext<Tenant, Role, User, ShopperCartDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ShopperCartDbContext(DbContextOptions<ShopperCartDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}
