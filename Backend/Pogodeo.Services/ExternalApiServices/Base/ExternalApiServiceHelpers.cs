using System.IO;
using System.Net;

namespace Pogodeo.Services
{
    /// <summary>
    /// Helpers that are used in external API services
    /// </summary>
    public static class ExternalApiServiceHelpers
    {
        /// <summary>
        /// The helper to build API URL address
        /// </summary>
        /// <param name="host">The host of a page</param>
        /// <param name="pathPrefix">The path to the exact API call</param>
        /// <param name="queryBeginning">The beginning of the query, usually API parameters</param>
        /// <param name="apikey">The api key to use</param>
        /// <returns></returns>
        public static string BuildUrl(string host, string pathPrefix, string queryBeginning, string apikeyName, string apikeyValue) => $"{host}{pathPrefix}{queryBeginning}{apikeyName}={apikeyValue}";

        /// <summary>
        /// Sends an API request to the server by specified url
        /// </summary>
        /// <param name="url">The ready url for API call</param>
        /// <returns>String with the response, usually JSON</returns>
        public static string SendAPIRequest(string url)
        {
            // Do the api call
            var request = (HttpWebRequest)WebRequest.Create(url);

            // Catch the response from external api
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                // Read the content as stream
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    // Return the stream as string
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
