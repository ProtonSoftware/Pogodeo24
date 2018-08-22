using Microsoft.EntityFrameworkCore;
using Pogodeo.DataAccess;
using System.Linq;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ForecastRepository : BaseRepository<TEST_Forecast, int>, IForecastRepository
    {
        public ForecastRepository(PogodeoAppDataContext db)
            : base(db) { }

        protected override DbSet<TEST_Forecast> DbSet => Db.Forecasts;

        public TEST_Forecast TESTGetWithIdFive()
        {
            return DbSet.Where(x => x.Id == 5).FirstOrDefault();
        }
    }
}
