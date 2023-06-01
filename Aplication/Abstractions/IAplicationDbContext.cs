using Domain.Models;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Abstractions
{
    public interface IAplicationDbContext
    {
        //Product
        DbSet<Categories> Categories { get; }
        DbSet<Products> Products { get; }
        DbSet<Orders> Orders { get; }
        DbSet<Waiter> Waiter { get; }
        DbSet<OrderProduct> OrderProduct { get; }
        DbSet<WaiterOrder> WaiterOrder { get; }

        // JWT 
        DbSet<Users> Users { get; }
        DbSet<Roles> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<UserRefreshToken> UserRefreshToken { get; }
        Task<int> SaveChangesAsync(CancellationToken token = default);

    }
}
