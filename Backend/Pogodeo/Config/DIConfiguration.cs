using Microsoft.Extensions.DependencyInjection;
using Pogodeo.Services;

namespace Pogodeo
{
    /// <summary>
    /// Provides initial configuration to the dependency injection
    /// </summary>
    public static class DIConfiguration
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            // Add services to the collection
            services.AddScoped<IBigCitiesRepository, BigCitiesRepository>();
            services.AddScoped<ISmallCitiesRepository, SmallCitiesRepository>();
            services.AddScoped<IOpenCageGeocoderService, OpenCageGeocoderService>();
            services.AddScoped<ICityFacade, CityFacade>();
            services.AddScoped<IAccuWeatherApiService, AccuWeatherApiService>();
            services.AddScoped<CityMapper, CityMapper>();

            // Return services for chaining
            return services;
        }
    }
}
