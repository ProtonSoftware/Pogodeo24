using Pogodeo.Core;
using System;

namespace Pogodeo.Services
{
    public interface ICityFacade
    {
        BigCityContext GetWeatherCity(string city);
        OperationResult UpdateWeatherIfNecessery(string city);
        bool CheckWeatherDate(DateTime date);
    }
}
