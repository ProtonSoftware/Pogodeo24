using System.Net.Http;
using System.Web.Http;

namespace Pogodeo.Controllers
{
    /// <summary>
    /// The main API controller for weather informations
    /// </summary>
    public class WeatherApiController : ApiController
    {
        /// <summary>
        /// API call for getting general weather informations about specified city
        /// </summary>
        /// <param name="city">The city to get weather info for</param>
        /// <returns>JSON with weather info, or error if city was invalid</returns>
        public HttpResponseMessage GetWeatherInfoForCity(string city)
        {
            return Request.CreateResponse("");
        }
    }
}
