using Microsoft.EntityFrameworkCore;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// This application's database context
    /// </summary>
    public class PogodeoAppDataContext : DbContext
    {
        #region Db Sets

        /// <summary>
        /// Big cities informations table
        /// </summary>
        public DbSet<BigCity> BigCitiesData { get; set; }

        /// <summary>
        /// Small cities association table
        /// </summary>
        public DbSet<SmallCity> SmallCitiesData { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PogodeoAppDataContext(DbContextOptions<PogodeoAppDataContext> options) : base(options)
        {
        
        }

        #endregion
    }
}
