namespace Pogodeo.Core
{
    /// <summary>
    /// Manages the routes for Api calls
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// Route to the GetWeatherForCity call
        /// </summary>
        public const string GetWeatherForCity = "/api/GetWeatherFor";

        /// <summary>
        /// Route to the CheckIfWeatherRequiresUpdate call
        /// </summary>
        public const string CheckIfWeatherRequiresUpdate = "/api/CheckWeatherUpdate";
    }
}
