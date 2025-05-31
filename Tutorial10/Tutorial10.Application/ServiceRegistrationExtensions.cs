using Microsoft.Extensions.DependencyInjection;
using Tutorial10.Application.Mappers;
using Tutorial10.Application.Mappers.Impl;
using Tutorial10.Application.Services;
using Tutorial10.Application.Services.Impl;

namespace Tutorial10.Application;

public static class ServiceRegistrationExtensions
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection services)
   {
       return services.AddServices();
   }

   private static IServiceCollection AddServices(this IServiceCollection services)
   {
      return services.AddScoped<IDoctorService, DoctorService>()
                     .AddScoped<IPatientService, PatientService>()
                     .AddScoped<IMedicamentService, MedicamentService>()
                     .AddScoped<IPrescriptionService, PrescriptionService>();
   }

   private static IServiceCollection AddMappers(this IServiceCollection services)
   {
       return services.AddScoped<IDoctorMapper, DoctorMapper>()
                      .AddScoped<IPrescriptionMedicamentMapper, PrescriptionMedicamentMapper>()
                      .AddScoped<IPrescriptionMapper, PrescriptionMapper>();
   }
}