using Microsoft.Extensions.DependencyInjection;
using Tutorial9.Application.Mappers;
using Tutorial9.Application.Mappers.Impl;
using Tutorial9.Application.Services;
using Tutorial9.Application.Services.Impl;
using Tutorial9.Application.Utils;

namespace Tutorial9.Application;

public static class ApplicationServicesRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.AddServices()
                       .AddMappers();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<ITripService, TripService>()
                       .AddScoped<IClientService, ClientService>()
                       .AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
    
    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services.AddScoped<ITripMapper, TripMapper>()
                       .AddScoped<ICountryMapper, CountryMapper>()
                       .AddScoped<IClientMapper, ClientMapper>();
    }
}