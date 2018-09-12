using Microsoft.EntityFrameworkCore;
using Pogodeo.DataAccess;
using System;

namespace Pogodeo.Tests
{
    /// <summary>
    /// The base class for every database tester
    /// </summary>
    public class BaseDatabaseTests : IDisposable
    {
        #region Setup

        /// <summary>
        /// The database that is used in the application
        /// </summary>
        protected PogodeoAppDataContext DatabaseContext { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDatabaseTests()
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
    }
}
