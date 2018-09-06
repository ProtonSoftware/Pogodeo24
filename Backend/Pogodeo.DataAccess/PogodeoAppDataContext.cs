using Microsoft.EntityFrameworkCore;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class PogodeoAppDataContext : DbContext
    {
        #region Db Sets

        public DbSet<CityLocalizationKeys> CityLocalizationKeys { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PogodeoAppDataContext(DbContextOptions<PogodeoAppDataContext> options) : base(options)
        {
        
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
