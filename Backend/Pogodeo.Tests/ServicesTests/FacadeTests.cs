using Microsoft.Extensions.Configuration;
using Pogodeo.Services;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Pogodeo.Tests
{
    /// <summary>
    /// The unit tests for facade classes
    /// </summary>
    public class FacadeTests : BaseDatabaseTests
    {
        [Fact]
        public void CityFacade_ShouldGetWeatherCityTheSameForBigCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);
            var smallRepository = new SmallCitiesRepository(DatabaseContext, mapper);
            var openCageService = new OpenCageGeocoderService(mockConfiguration);
            var accuWeatherService = new AccuWeatherApiService(mockConfiguration, bigRepository);
            var aerisWeatherService = new AerisWeatherApiService(mockConfiguration);
            var wwoService = new WWOApiService(mockConfiguration);

            var facade = new CityFacade(mapper, mockConfiguration, openCageService, bigRepository, smallRepository, accuWeatherService, aerisWeatherService, wwoService);

            // Get the big city
            var result = facade.GetWeatherCity("Przemyśl");

            // Check
            Assert.Equal("Przemyśl", result.CityName);
        }

        [Fact]
        public void CityFacade_ShouldGetWeatherCityDifferentForSmallCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);
            var smallRepository = new SmallCitiesRepository(DatabaseContext, mapper);
            var openCageService = new OpenCageGeocoderService(mockConfiguration);
            var accuWeatherService = new AccuWeatherApiService(mockConfiguration, bigRepository);
            var aerisWeatherService = new AerisWeatherApiService(mockConfiguration);
            var wwoService = new WWOApiService(mockConfiguration);

            var facade = new CityFacade(mapper, mockConfiguration, openCageService, bigRepository, smallRepository, accuWeatherService, aerisWeatherService, wwoService);

            // Get the big city from small one
            var result = facade.GetWeatherCity("Jarosław");

            // Check
            Assert.NotEqual("Jarosław", result.CityName);
        }

        [Fact]
        public void CityFacade_ShouldGetNewWeatherForBigCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);
            var smallRepository = new SmallCitiesRepository(DatabaseContext, mapper);
            var openCageService = new OpenCageGeocoderService(mockConfiguration);
            var accuWeatherService = new AccuWeatherApiService(mockConfiguration, bigRepository);
            var aerisWeatherService = new AerisWeatherApiService(mockConfiguration);
            var wwoService = new WWOApiService(mockConfiguration);

            var facade = new CityFacade(mapper, mockConfiguration, openCageService, bigRepository, smallRepository, accuWeatherService, aerisWeatherService, wwoService);

            // Update the city based on name
            var cityResult = facade.UpdateWeatherIfNecessery("Przemyśl");

            // Get the weather
            var weatherResult = facade.GetWeatherCity("Przemyśl");

            // Check
            Assert.True(cityResult.Success);
            Assert.NotNull(weatherResult.AccuWeatherContext.NextDaysWeatherTruncatedData);
            Assert.NotNull(weatherResult.AccuWeatherContext.TodayWeatherTruncatedData);
            Assert.NotNull(weatherResult.AerisWeatherContext.NextDaysWeatherTruncatedData);
            Assert.NotNull(weatherResult.AerisWeatherContext.TodayWeatherTruncatedData);
            Assert.NotNull(weatherResult.WWOContext.NextDaysWeatherTruncatedData);
            Assert.NotNull(weatherResult.WWOContext.TodayWeatherTruncatedData);
        }

        [Fact]
        public void CityFacade_ShouldGetNewWeatherForSmallCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);
            var smallRepository = new SmallCitiesRepository(DatabaseContext, mapper);
            var openCageService = new OpenCageGeocoderService(mockConfiguration);
            var accuWeatherService = new AccuWeatherApiService(mockConfiguration, bigRepository);
            var aerisWeatherService = new AerisWeatherApiService(mockConfiguration);
            var wwoService = new WWOApiService(mockConfiguration);

            var facade = new CityFacade(mapper, mockConfiguration, openCageService, bigRepository, smallRepository, accuWeatherService, aerisWeatherService, wwoService);

            // Update the city based on name
            var cityResult = facade.UpdateWeatherIfNecessery("Jarosław");

            // Get the weather
            var weatherResult = facade.GetWeatherCity("Jarosław");

            // Check
            Assert.True(cityResult.Success);
            Assert.NotNull(weatherResult.AccuWeatherContext.NextDaysWeatherTruncatedData);
            Assert.NotNull(weatherResult.AccuWeatherContext.TodayWeatherTruncatedData);
            Assert.NotNull(weatherResult.AerisWeatherContext.NextDaysWeatherTruncatedData);
            Assert.NotNull(weatherResult.AerisWeatherContext.TodayWeatherTruncatedData);
            Assert.NotNull(weatherResult.WWOContext.NextDaysWeatherTruncatedData);
            Assert.NotNull(weatherResult.WWOContext.TodayWeatherTruncatedData);
        }

        [Fact]
        public void CityFacade_ShouldNotGetWeatherForRandomCity()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);
            var smallRepository = new SmallCitiesRepository(DatabaseContext, mapper);
            var openCageService = new OpenCageGeocoderService(mockConfiguration);
            var accuWeatherService = new AccuWeatherApiService(mockConfiguration, bigRepository);
            var aerisWeatherService = new AerisWeatherApiService(mockConfiguration);
            var wwoService = new WWOApiService(mockConfiguration);

            var facade = new CityFacade(mapper, mockConfiguration, openCageService, bigRepository, smallRepository, accuWeatherService, aerisWeatherService, wwoService);

            // Update the city based on name
            var result = facade.UpdateWeatherIfNecessery("DSADSADSADSA");

            // Check
            Assert.False(result.Success);
        }

        [Fact]
        public void CityFacade_ShouldGetUpdateForOutdatedDate()
        {
            // Arrange
            var mockConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testappsettings.json").Build();

            var mapper = new CityMapper();
            var bigRepository = new BigCitiesRepository(DatabaseContext, mapper);
            var smallRepository = new SmallCitiesRepository(DatabaseContext, mapper);
            var openCageService = new OpenCageGeocoderService(mockConfiguration);
            var accuWeatherService = new AccuWeatherApiService(mockConfiguration, bigRepository);
            var aerisWeatherService = new AerisWeatherApiService(mockConfiguration);
            var wwoService = new WWOApiService(mockConfiguration);

            var facade = new CityFacade(mapper, mockConfiguration, openCageService, bigRepository, smallRepository, accuWeatherService, aerisWeatherService, wwoService);

            // Get the big city from small one
            var nowResult = facade.CheckWeatherDate(DateTime.Now);
            var earlierResult = facade.CheckWeatherDate(DateTime.Now - new TimeSpan(2, 0, 0));
            var earlierEarlierResult = facade.CheckWeatherDate(DateTime.Now - new TimeSpan(5, 0, 0));
            var outdatedResult = facade.CheckWeatherDate(DateTime.Now - new TimeSpan(6, 0, 1));

            // Check
            Assert.True(nowResult);
            Assert.True(earlierResult);
            Assert.True(earlierEarlierResult);
            Assert.False(outdatedResult);
        }
    }
}
