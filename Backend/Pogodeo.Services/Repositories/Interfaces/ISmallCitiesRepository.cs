using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    public interface ISmallCitiesRepository : IRepository<SmallCity, int>
    {
        SmallCityContext GetByName(string city);
    }
}
