using System;

namespace Pogodeo.Services
{
    /// <summary>
    /// The facade that handles city operations such as associating small cities with big, database calls etc.
    /// </summary>
    public class CityFacade : ICityFacade
    {
        #region Private Members

        /// <summary>
        /// The service that gets us city information from OpenCageGeocoder API
        /// </summary>
        private readonly IOpenCageGeocoderService mOpenCageGeocoderService;

        /// <summary>
        /// The repository for big cities database table
        /// </summary>
        private readonly IBigCitiesRepository mBigCitiesRepository;

        // <summary>
        /// The repository for small cities database table
        /// </summary>
        private readonly ISmallCitiesRepository mSmallCitiesRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CityFacade(IOpenCageGeocoderService openCageGeocoderService, IBigCitiesRepository bigCitiesRepository, ISmallCitiesRepository smallCitiesRepository)
        {
            mOpenCageGeocoderService = openCageGeocoderService;
            mBigCitiesRepository = bigCitiesRepository;
            mSmallCitiesRepository = smallCitiesRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the big city that we should get weather for
        /// </summary>
        /// <param name="city">The user's provided city</param>
        /// <returns>The big city from our database as string</returns>
        public string GetWeatherCity(string city)
        {
            // Check if we have this city in our big city database
            var bigCity = mBigCitiesRepository.GetByName(city);
            
            // If none was found
            if (bigCity == null)
            {
                // Check if we have this city in our small city database
                var smallCity = mSmallCitiesRepository.GetByName(city);

                // If none was found
                if (smallCity == null)
                {
                    // Send Api request
                    SendApiRequest(city);

                    // Get newly saved small city
                    smallCity = mSmallCitiesRepository.GetByName(city);

                    // If none was found
                    if (smallCity == null)
                        // City doesn't exist
                        return null;
                }

                // Return it's associated big city name
                return smallCity.AssociatedBigCity.CityName;
            }

            // Otherwise, return it's name
            return bigCity.CityName;
        }

        #endregion

        /// <summary>
        /// Sends an API request to find out if city exists and saves it in our database
        /// </summary>
        /// <param name="city">The name of a city to look up for</param>
        private void SendApiRequest(string city)
        {
            // Send API request and catch the response
            var response = mOpenCageGeocoderService.GetAPIInfo(city);

            // If we didn't get a city
            if (!response.Success || response.Result == null)
                // City doesn't exist
                return;

            // Otherwise, get the associated big city
            mBigCitiesRepository.AttachNewSmallCity(response.Result.Name, response.Result.Latitude, response.Result.Longitude);
        }
    }
}
