using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    public interface IBigCitiesRepository : IRepository<BigCity, int>
    {
        BigCityContext GetByName(string city);

        void AttachNewSmallCity(string name, string latitude, string longitude);
        string GetAccuWeatherLocalizationKey(string city);
        void UpdateAccuWeatherLocalizationKey(string city, string localizationKey);
    }
}
