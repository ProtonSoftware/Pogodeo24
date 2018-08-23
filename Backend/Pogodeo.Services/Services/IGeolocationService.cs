namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeolocationService
    {
        Geography GetLocationFromName(string name);
    }
}
