using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The Db context for this application
    /// Used to cache weather data in database
    /// </summary>
    public class PogodeoMobileDbContext : DbContext
    {
        #region Db Sets

        /// <summary>
        /// The table for cached cities' weather
        /// </summary>
        public DbSet<CityWeather> CityWeather { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PogodeoMobileDbContext() : base()
        {
        }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "database.sqlite")}");
        }
    }
}
