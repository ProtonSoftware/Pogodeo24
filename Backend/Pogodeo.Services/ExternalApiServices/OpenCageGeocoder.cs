using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pogodeo.DataAccess;
using System;
using System.IO;
using System.Net;

namespace Pogodeo.Services.ExternalApiServices
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenCageGeocoder : IOpenCageGeocoder
    {
        private readonly string host;
        private readonly string apiKey;
        private readonly string responseType;
        private readonly string pathPrefix;
        private readonly string queryAddresParamName;

        public OpenCageGeocoder(IConfiguration config)
        {
            var section = config.GetSection("TheOpenCageGeocoderAPI").GetSection("Config");
            host = section.GetValue<string>("Host");
            pathPrefix = section.GetValue<string>("PathPrefix");
            queryAddresParamName = section.GetValue<string>("AddresQueryParamName");
            apiKey = section.GetValue<string>("APIKey");
            responseType = section.GetValue<string>("DefaultResponseFormat");
        }

        public OperationResult<HttpWebResponse> GetAddressLocation(string address)
        {
            var url = BuildUrl(address);
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            return new OperationResult<HttpWebResponse>(true, response);
        }

        private string BuildUrl(string placeName)
        {
            var builder = new UriBuilder(host)
            {
                Path = pathPrefix + responseType,
                Query = $"key={apiKey}&{queryAddresParamName}={placeName}",
            };

            var url = builder.ToString();

            return url;
        }
    }
}
