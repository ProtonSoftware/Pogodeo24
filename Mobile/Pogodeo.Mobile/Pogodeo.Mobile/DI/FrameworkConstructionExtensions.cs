using Dna;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for Pogodeo mobile application
        /// </summary>
        /// <param name="construction">Framework's construction</param>
        public static FrameworkConstruction AddPogodeoViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of City Weather repository
            construction.Services.AddSingleton<ICityWeatherRepository, CityWeatherRepository>();

            // Bind to a single instance of City mapper
            construction.Services.AddSingleton<CityMapper>();

            // Return the construction for chaining
            return construction;
        }

        /// <summary>
        /// Injects the database for Pogodeo mobile application
        /// </summary>
        /// <param name="construction">Framework's construction</param>
        public static FrameworkConstruction AddDbContext(this FrameworkConstruction construction)
        {
            construction.Services.AddEntityFrameworkSqlite();

            // Bind a db context to access in this application
            construction.Services.AddDbContext<PogodeoMobileDbContext>();

            var serviceProvider = construction.Services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PogodeoMobileDbContext>();
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }

            // Return the construction for chaining
            return construction;
        }
    }
}
