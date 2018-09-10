using Pogodeo.Core;

namespace Pogodeo.Services
{
    /// <summary>
    /// Base interface to implement by every external API service in this app
    /// </summary>
    public interface IBaseExternalApiService<TResponse>
    {
        string Host { get; }
        string ApiKeyName { get; }
        string ApiKeyValue { get; }

        OperationResult<TResponse> GetAPIInfo(string city);
    }
}
