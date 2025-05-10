using Tutorial8.Repositories;
using Tutorial8.Repositories.Impl;
using Tutorial8.Services;
using Tutorial8.Services.Impl;
using Tutorial8.Utils;

namespace Tutorial8.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services.RegisterRepositories();
        services.RegisterServices();
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IProductWarehouseService, ProductWarehouseService>();
        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        return services;
    }
}