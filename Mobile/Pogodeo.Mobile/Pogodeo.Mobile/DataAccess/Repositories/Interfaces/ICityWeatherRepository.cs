using Pogodeo.Core;
using System.Collections.Generic;

namespace Pogodeo.Mobile
{
    public interface ICityWeatherRepository : IRepository<CityWeather, int>
    {
        void SaveWeatherForCity(string city, Dictionary<APIProviderType, WeatherInformationAPIModel> weather);
        CityWeatherContext GetCityWeather(string city);
    }
}
