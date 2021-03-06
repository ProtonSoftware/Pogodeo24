﻿using System.Collections.Generic;

namespace Pogodeo.Core
{
    /// <summary>
    /// The model for API weather response object
    /// </summary>
    public class APIWeatherResponse
    {
        /// <summary>
        /// The list of informations about weather from external APIs
        /// </summary>
        public List<WeatherInformationAPIModel> WeatherInformationsList { get; set; }
    }
}
