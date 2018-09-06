namespace Pogodeo.Services
{
    public interface IAccuWeatherApiService : IBaseExternalApiService
    {
        string LocalizationKeyPath { get; }
        string WeatherPath { get; }
    }
}
