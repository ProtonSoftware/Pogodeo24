using Pogodeo.Core;
using System.Net;

namespace Pogodeo.Services.ExternalApiServices
{
    public interface IOpenCageGeocoder
    {
        OperationResult<HttpWebResponse> GetAddressLocation(string address);
    }
}
