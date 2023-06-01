using Aplication.Interfaces.InterfacesJWT;
using Aplication.Interfaces.InterfacesProducts;
using Aplication.Services.ServicesJwt;
using Aplication.Services.ServicesProduct;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication
{
    public static class Startup
    {
        public static IServiceCollection AddAplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IOrderProductRepository, OrderProductRepository>();
            service.AddScoped<IOrdersRepository, OrderRepository>();
            
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IWaiterOrderRepository, WaiterOrderRepository>();
            service.AddScoped<IWaiterRepository, WaiterRepository>();

            service.AddScoped<IPermissionRepository, PermissionRepository>();
            service.AddScoped<IPermissionRoleRepository, PermissionRoleRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUserRoleRepository, UserRoleRepository>();
            service.AddTransient<IJwtService, JwtService>();
            service.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

            return service;
        }
    }
}
