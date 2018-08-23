using Pogodeo.DataAccess;
using System.Net;

namespace Pogodeo.Services.ExternalApiServices
{
    public interface IOpenCageGeocoder
    {
        OperationResult<HttpWebResponse> GetAddressLocation(string address);
    }
}
