using Aplication.Abstractions;
using Domain.Models;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class ProductDbContext : DbContext, IAplicationDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        //Products
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Waiter> Waiter { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }

        public DbSet<WaiterOrder> WaiterOrder { get; set; }

        // JWT 
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
    }
}
