using Microsoft.Extensions.DependencyInjection;
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
      return services.AddScoped<IPrescriptionService, PrescriptionService>();
   }
}