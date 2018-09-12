using Microsoft.EntityFrameworkCore;
using Pogodeo.DataAccess;
using Pogodeo.Services;
using System;
using Xunit;

namespace Pogodeo.Tests
{
    /// <summary>
    /// The unit tests for repository classes
    /// </summary>
    public class RepositoryTests : IDisposable
    {
        #region Setup

        /// <summary>
        /// The database that is used in the application
        /// </summary>
        protected PogodeoAppDataContext DatabaseContext { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RepositoryTests()
        {
            // Options for the database to use
            var options = new DbContextOptionsBuilder<PogodeoAppDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            // Create the database
            DatabaseContext = new PogodeoAppDataContext(options);

            // Make sure its created properly
            DatabaseContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Dispose after every test
        /// </summary>
        public void Dispose()
        {
            // Make sure database is deleted
            DatabaseContext.Database.EnsureDeleted();

            // Dispose the database
            DatabaseContext.Dispose();
        }

        #endregion

        [Fact]
        public void BigCitiesRepository_ShouldGetLocalizationKeyForKnownCity()
        {
            // Arrange
            var repository = new BigCitiesRepository(DatabaseContext, new CityMapper());

            // Get the localization key
            var result = repository.GetAccuWeatherLocalizationKey("Przemyœl");

            // Check
            Assert.NotNull(result);
        }

        [Fact]
        public void BigCitiesRepository_ShouldNotGetLocalizationKeyForUnknownCity()
        {
            // Arrange
            var repository = new BigCitiesRepository(DatabaseContext, new CityMapper());

            // Get the localization key
            var result = repository.GetAccuWeatherLocalizationKey("Jaros³aw");

            // Check
            Assert.Null(result);
        }

        [Fact]
        public void BigCitiesRepository_ShouldNotGetLocalizationKeyForRandomCity()
        {
            // Arrange
            var repository = new BigCitiesRepository(DatabaseContext, new CityMapper());

            // Get the localization key
            var result = repository.GetAccuWeatherLocalizationKey("ADSADSADSADSADSA");

            // Check
            Assert.Null(result);
        }

        [Fact]
        public void BigCitiesRepository_ShouldGetEveryBigCity()
        {
            // Arrange
            var repository = new BigCitiesRepository(DatabaseContext, new CityMapper());

            // Get the first city
            var cityResult = repository.GetByName("Przemyœl");

            // Get the 78th city, as there should be at least that amount
            var idResult = repository.GetById(78);
            
            // Check
            Assert.NotNull(cityResult);
            Assert.NotNull(idResult);
        }

        [Fact]
        public void BigCitiesRepository_ShouldUpdateLocalizationKey()
        {
            // Arrange
            var repository = new BigCitiesRepository(DatabaseContext, new CityMapper());

            // Update the localization key
            repository.UpdateAccuWeatherLocalizationKey("Przemyœl", "TEST_LocalizationKey");

            // Get localization key from that city
            var result = repository.GetAccuWeatherLocalizationKey("Przemyœl");

            // Check
            Assert.Equal("TEST_LocalizationKey", result);
        }

        [Fact]
        public void SmallCitiesRepository_ShouldNotGetNonExistingCity()
        {
            // Arrange
            var repository = new SmallCitiesRepository(DatabaseContext, new CityMapper());

            // Get the city
            var result = repository.GetByName("Jaros³aw");

            // Check
            Assert.Null(result);
        }

        [Fact]
        public void SmallCitiesRepository_ShouldAddAndGetCity()
        {
            // Arrange
            var bigRepository = new BigCitiesRepository(DatabaseContext, new CityMapper());
            var smallRepository = new SmallCitiesRepository(DatabaseContext, new CityMapper());

            // Add new city
            bigRepository.AttachNewSmallCity("Jaros³aw", "50", "22.61");

            // Get the city
            var result = smallRepository.GetByName("Jaros³aw");

            // Check
            Assert.NotNull(result);
        }
    }
}
