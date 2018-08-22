using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TestService : ITestService
    {
        private IForecastRepository _forecasts;

        public TestService(IForecastRepository forecasts)
        {
            _forecasts = forecasts;
        }

        public TEST_Forecast GetForecastForCity(string city)
        {
            return _forecasts.TESTGetWithIdFive();
        }
    }
}
