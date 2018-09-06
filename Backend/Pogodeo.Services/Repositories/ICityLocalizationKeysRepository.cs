using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    public interface ICityLocalizationKeysRepository : IRepository<CityLocalizationKeys, int>
    {
        string GetAccuWeatherLocalizationKey(string city);
    }
}
