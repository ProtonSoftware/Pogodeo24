using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IForecastRepository : IRepository<TEST_Forecast, int>
    {
        TEST_Forecast TESTGetWithIdFive();
    }
}
