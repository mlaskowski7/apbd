using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutorial9.Application.Repositories;
using Tutorial9.Infrastructure.Repositories.Impl;

namespace Tutorial9.Infrastructure;

public static class InfServicesRegistrationExtensions
{
    public static IServiceCollection AddInfServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TripsDatabaseContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection") 
                             ?? throw new ArgumentException("Database default connection string must be set"));
        });
        
        services.AddScoped<ITripRepository, TripRepository>()
                .AddScoped<IClientRepository, ClientRepository>();
        
        return services;
    }
}