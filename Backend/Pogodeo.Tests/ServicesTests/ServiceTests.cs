using Microsoft.Extensions.Configuration;
using Pogodeo.Services;
using System.IO;
using Xunit;

namespace Pogodeo.Tests
{
    /// <summary>
    /// The unit tests for service classes
    /// </summary>
    public class ServiceTests : BaseDatabaseTests
    {
        [Fact]
        public void AccuWeatherService_ShouldGetConfigurationError()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder().Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);

            var service = new AccuWeatherApiService(mockConfiguration, bigRepository);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.False(result.Success);
        }

        [Fact]
        public void AccuWeatherService_ShouldSuccessForRightConfiguration()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);

            var service = new AccuWeatherApiService(mockConfiguration, bigRepository);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.True(result.Success);
        }

        [Fact]
        public void AccuWeatherService_ShouldGetWeatherForBigCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);

            var service = new AccuWeatherApiService(mockConfiguration, bigRepository);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.True(result.Result.TodayWeatherTruncatedData.Count >= 6);
            Assert.True(result.Result.NextDaysWeatherTruncatedData.Count >= 5);
        }

        [Fact]
        public void AccuWeatherService_ShouldNotGetWeatherForRandomCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);

            var service = new AccuWeatherApiService(mockConfiguration, bigRepository);

            // Try to get weather
            var result = service.GetAPIInfo("DSADSADSADSADAS");

            // Check
            Assert.False(result.Success);
        }

        [Fact]
        public void AerisWeatherService_ShouldGetConfigurationError()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder().Build();

            var service = new AerisWeatherApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.False(result.Success);
        }

        [Fact]
        public void AerisWeatherService_ShouldSuccessForRightConfiguration()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var service = new AerisWeatherApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.True(result.Success);
        }

        [Fact]
        public void AerisWeatherService_ShouldGetWeatherForBigCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var service = new AerisWeatherApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.True(result.Result.TodayWeatherTruncatedData.Count >= 6);
            Assert.True(result.Result.NextDaysWeatherTruncatedData.Count >= 5);
        }

        [Fact]
        public void AerisWeatherService_ShouldNotGetWeatherForRandomCity()
        {
            /// Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var service = new AerisWeatherApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("DSADSADSADSADAS");

            // Check
            Assert.False(result.Success);
        }

        [Fact]
        public void WWOService_ShouldGetConfigurationError()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder().Build();

            var service = new WWOApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.False(result.Success);
        }

        [Fact]
        public void WWOService_ShouldSuccessForRightConfiguration()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var service = new WWOApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.True(result.Success);
        }

        [Fact]
        public void WWOService_ShouldGetWeatherForBigCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var service = new WWOApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("Przemyśl");

            // Check
            Assert.True(result.Result.TodayWeatherTruncatedData.Count >= 6);
            Assert.True(result.Result.NextDaysWeatherTruncatedData.Count >= 5);
        }

        [Fact]
        public void WWOService_ShouldNotGetWeatherForRandomCity()
        {
            /// Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var service = new WWOApiService(mockConfiguration);

            // Try to get weather
            var result = service.GetAPIInfo("DSADSADSADSADAS");

            // Check
            Assert.False(result.Success);
        }
    }
}
