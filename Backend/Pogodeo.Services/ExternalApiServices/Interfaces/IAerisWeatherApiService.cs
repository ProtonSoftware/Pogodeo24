namespace Pogodeo.Services
{
    public interface IAerisWeatherApiService : IBaseExternalApiService
    {
        string ClientID { get; }
        string WeatherPath { get; }
    }
}
