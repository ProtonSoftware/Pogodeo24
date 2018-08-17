using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Pogodeo
{
    /// <summary>
    /// The main API controller for weather informations
    /// </summary>
    public class WeatherApiController : Controller
    {
        /// <summary>
        /// API call for getting general weather informations about specified city
        /// </summary>
        /// <param name="city">The city to get weather info for</param>
        /// <returns>JSON with weather info, or error if city was invalid</returns>
        public HttpResponseMessage GetWeatherInfoForCity(string city)
        {
            return new HttpResponseMessage();
        }
    }
}
