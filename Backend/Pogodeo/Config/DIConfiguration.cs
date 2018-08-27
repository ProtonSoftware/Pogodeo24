using Microsoft.Extensions.DependencyInjection;
using Pogodeo.Services;
using Pogodeo.Services.ExternalApiServices;

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
            services.AddScoped<IForecastRepository, ForecastRepository>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IOpenCageGeocoder, OpenCageGeocoder>();
            services.AddScoped<IGeolocationService, GeolocationService>();

            // Return services for chaining
            return services;
        }
    }
}
