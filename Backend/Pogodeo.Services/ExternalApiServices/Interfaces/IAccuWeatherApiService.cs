namespace Pogodeo.Services
{
    public interface IAccuWeatherApiService : IBaseExternalApiService
    {
        string LocalizationKeyPath { get; }
        string WeatherHourPath { get; }
        string WeatherDayPath { get; }
    }
}
