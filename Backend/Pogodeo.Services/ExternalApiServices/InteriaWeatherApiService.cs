using Pogodeo.Core;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Pogodeo.Services
{
    /// <summary>
    /// The service for interia api calls that gets us weather info
    /// </summary>
    public class InteriaWeatherApiService : IInteriaWeatherApiService
    {
        private readonly string host;
        private readonly string apiKey;
        private readonly string pathPrefix;
        private readonly string queryAddresParamName;

        public InteriaWeatherApiService(IConfiguration config)
        {
            var section = config.GetSection("AccuWeatherAPI").GetSection("Config");
            host = section.GetValue<string>("Host");
            pathPrefix = section.GetValue<string>("PathPrefix");
            queryAddresParamName = section.GetValue<string>("AddresQueryParamName");
            apiKey = section.GetValue<string>("APIKey");
        }

        public OperationResult<HttpWebResponse> GetWeatherInfo(string city)
        {
            var url = BuildUrl(city);
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            return new OperationResult<HttpWebResponse>(true, response);
        }

        private string BuildUrl(string placeName)
        {
            var builder = new UriBuilder(host)
            {
                Path = pathPrefix,
                Query = $"{queryAddresParamName}={placeName}&apikey={apiKey}",
            };

            var url = builder.ToString();

            return url;
        }
    }
}
