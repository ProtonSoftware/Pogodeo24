using Pogodeo.DataAccess;
using Pogodeo.Services;
using Xunit;

namespace Pogodeo.Tests
{
    // <summary>
    /// The unit tests for mapper classes
    /// </summary>
    public class MapperTests
    {
        [Fact]
        public void CityMapper_ShouldMapBigCityEntityToContext()
        {
            // Arrange
            var mapper = new CityMapper();

            // Create the entity
            var entity = new BigCity
            {
                CityName = "Przemyśl",
                Latitude = "49.787",
                Longitude = "22.79",
                AccuWeatherLocalizationKey = "1-275041_1_AL"
            };

            // Map it to context
            var result = mapper.Map(entity);

            // Check
            Assert.NotNull(result);
            Assert.NotNull(result.CityName);
            Assert.NotNull(result.Latitude);
            Assert.NotNull(result.Longitude);
        }

        [Fact]
        public void CityMapper_ShouldMapSmallCityEntityToContext()
        {
            // Arrange
            var mapper = new CityMapper();

            // Create the entity
            var entity = new SmallCity
            {
                CityName = "Jarosław",
                Latitude = "50",
                Longitude = "22.61",
                AssociatedBigCity = new BigCity
                {
                    CityName = "Przemyśl",
                    Latitude = "49.787",
                    Longitude = "22.79",
                    AccuWeatherLocalizationKey = "1-275041_1_AL"
                }
            };

            // Map it to context
            var result = mapper.Map(entity);

            // Check
            Assert.NotNull(result);
            Assert.NotNull(result.CityName);
            Assert.NotNull(result.Latitude);
            Assert.NotNull(result.Longitude);
            Assert.NotNull(result.AssociatedBigCity);
            Assert.NotNull(result.AssociatedBigCity.CityName);
            Assert.NotNull(result.AssociatedBigCity.Latitude);
            Assert.NotNull(result.AssociatedBigCity.Longitude);
        }
    }
}
