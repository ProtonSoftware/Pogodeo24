using Microsoft.EntityFrameworkCore;
using Pogodeo.DataAccess;
using System.Linq;

namespace Pogodeo.Services
{
    /// <summary>
    /// The repository that handles small cities association with big ones in the database
    /// </summary>
    public class SmallCitiesRepository : BaseRepository<SmallCity, int>, ISmallCitiesRepository
    {
        #region Private Members

        /// <summary>
        /// The AutoMapper for city objects
        /// </summary>
        private readonly CityMapper mCityMapper;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Interface implementation to access the small cities table
        /// </summary>
        protected override DbSet<SmallCity> DbSet => Db.SmallCitiesData;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">This app's database</param>
        public SmallCitiesRepository(PogodeoAppDataContext db, CityMapper cityMapper) : base(db)
        {
            mCityMapper = cityMapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Looks for city with specified name
        /// </summary>
        /// <param name="city">The name of a city to look up for</param>
        /// <returns><see cref="SmallCityContext"/></returns>
        public SmallCityContext GetByName(string city)
        {
            // Get the entity from database
            var result = DbSet.Where(model => InsensitiveStringComparition(model.CityName, city)).FirstOrDefault();

            // If we didn't get one
            if (result == null)
                return null;

            // Otherwise, map it as context and return
            return mCityMapper.Map(result);
        }

        #endregion
    }
}
