using Pogodeo.Core;
using System.Net;

namespace Pogodeo.Services
{
    public interface IInteriaWeatherApiService
    {
        OperationResult<HttpWebResponse> GetWeatherInfo(string city);
    }
}
