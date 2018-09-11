using Pogodeo.Core;

namespace Pogodeo.Services
{
    public interface IWWOApiService : IBaseExternalApiService<WeatherInformationAPIModel>
    {
        string WeatherPath { get; }
    }
}
