using Pogodeo.Core;

namespace Pogodeo.Services
{
    public interface IAerisWeatherApiService : IBaseExternalApiService<WeatherInformationAPIModel>
    {
        string ClientID { get; }
        string WeatherPath { get; }
    }
}
