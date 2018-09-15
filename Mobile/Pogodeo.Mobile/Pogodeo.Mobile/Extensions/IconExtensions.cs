using Pogodeo.Core;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The extensions for application's icon types
    /// </summary>
    public static class IconExtensions
    {
        /// <summary>
        /// Converts <see cref="WeatherIconType"/> to a Weather Icons Font string
        /// </summary>
        /// <param name="type">The icon to convert</param>
        /// <returns>Weather Icons Font's icon string</returns>
        public static string ToWeatherFont(this WeatherIconType icon)
        {
            // Return a Font string based on the icon type
            switch (icon)
            {
                case WeatherIconType.Sun:
                    return "\uf00d";

                case WeatherIconType.Rain:
                    return "\uf019";

                case WeatherIconType.Cloud:
                    return "\uf041";

                case WeatherIconType.Storm:
                    return "\uf01e";

                // If none found, return null
                default:
                    return null;
            }
        }

        /// <summary>
        /// Converts <see cref="ApplicationIconType"/> to a Material Icon string
        /// </summary>
        /// <param name="type">The icon to convert</param>
        /// <returns>Material Icon string</returns>
        public static string ToMaterialFont(this ApplicationIconType icon)
        {
            // Return a Material Icon string based on the icon type
            switch (icon)
            {
                case ApplicationIconType.Settings:
                    return "\ue8b8";

                case ApplicationIconType.About:
                    return "\ue88e";

                // If none found, return null
                default:
                    return null;
            }
        }
    }
}
