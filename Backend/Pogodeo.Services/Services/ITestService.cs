using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITestService
    {
        TEST_Forecast GetForecastForCity(string city);
    }
}
