namespace Pogodeo.Services
{
    public interface IPogodynkaApiService : IBaseExternalApiService
    {
        string WeatherPath { get; }
    }
}
