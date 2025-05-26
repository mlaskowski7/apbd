using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        
        return services;
    }
}