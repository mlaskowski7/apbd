using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutorial10.Application.Persistence;
using Tutorial10.Application.Repositories;
using Tutorial10.Infrastructure.Database;
using Tutorial10.Infrastructure.Persistence;
using Tutorial10.Infrastructure.Repositories;

namespace Tutorial10.Infrastructure;

public static class ServiceRegistrationExtensions
{
   public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
   {
      return services.AddDbContext<ClinicDbContext>(opt =>
      {
         opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection") 
                          ?? throw new ArgumentException("Default connection string must be set"));
      }).AddRepositories().AddScoped<IUnitOfWork, UnitOfWork>();
   }

   private static IServiceCollection AddRepositories(this IServiceCollection services)
   {
      return services.AddScoped<IDoctorRepository, DoctorRepository>()
                     .AddScoped<IMedicamentRepository, MedicamentRepository>()
                     .AddScoped<IPatientRepository, PatientRepository>()
                     .AddScoped<IPrescriptionRepository, PrescriptionRepository>();
   }
}