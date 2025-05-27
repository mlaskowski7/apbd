using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutorial10.Infrastructure.Database;

namespace Tutorial10.Infrastructure;

public static class ServiceRegistrationExtensions
{
   public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
   {
      return services.AddDbContext<ClinicDbContext>(opt =>
      {
         opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection") 
                          ?? throw new ArgumentException("Default connection string must be set"));
      });
   } 
}