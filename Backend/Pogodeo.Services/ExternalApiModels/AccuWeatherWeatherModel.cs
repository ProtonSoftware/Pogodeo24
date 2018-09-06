using Newtonsoft.Json;
using System;

namespace Pogodeo.Services
{
    /// <summary>
    /// The model for AccuWeather weather info
    /// </summary>
    public class AccuWeatherWeatherModel
    {
        /// <summary>
        /// The date that this weather is associated with
        /// </summary>
        public DateTime Date { get; set; }
        
        public int Minimum { get; set; }
        
        public int Maximum { get; set; }
    }
}
