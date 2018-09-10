using Pogodeo.Core;

namespace Pogodeo.Services
{
    public interface IAccuWeatherApiService : IBaseExternalApiService<WeatherInformationAPIModel>
    {
        string LocalizationKeyPath { get; }
        string WeatherHourPath { get; }
        string WeatherDayPath { get; }
    }
}
