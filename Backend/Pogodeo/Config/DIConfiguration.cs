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

            services.AddScoped<ICityFacade, CityFacade>();

            services.AddScoped<CityMapper, CityMapper>();

            services.AddScoped<IOpenCageGeocoderService, OpenCageGeocoderService>();
            services.AddScoped<IAccuWeatherApiService, AccuWeatherApiService>();
            services.AddScoped<IAerisWeatherApiService, AerisWeatherApiService>();
            services.AddScoped<IWWOApiService, WWOApiService>();

            // Return services for chaining
            return services;
        }
    }
}
