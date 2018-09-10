using Pogodeo.Core;

namespace Pogodeo.Services
{
    public interface ICityFacade
    {
        BigCityContext GetWeatherCity(string city);
        OperationResult UpdateWeatherIfNecessery(string city);
    }
}
