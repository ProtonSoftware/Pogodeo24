using Pogodeo.Core;
using System.Net;

namespace Pogodeo.Services
{
    /// <summary>
    /// Base interface to implement by every external API service in this app
    /// </summary>
    public interface IBaseExternalApiService
    {
        string Host { get; }
        string ApiKeyName { get; }
        string ApiKeyValue { get; }

        OperationResult<object> GetAPIInfo(string city);
    }
}
