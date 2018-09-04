using Pogodeo.Core;
using System.Net;

namespace Pogodeo.Services
{
    public interface IOpenCageGeocoder
    {
        OperationResult<HttpWebResponse> GetAddressLocation(string address);
    }
}
