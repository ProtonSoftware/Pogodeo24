using Newtonsoft.Json;

namespace Pogodeo
{
    /// <summary>
    /// Helper extensions to deal with Json
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts an object to the Json
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns>Json as string</returns>
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    }
}
