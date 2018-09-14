using AutoMapper;
using Newtonsoft.Json;
using Pogodeo.Core;
using System.Collections.Generic;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The mapper for city objects
    /// </summary>
    public class CityMapper
    {
        #region Private Members

        /// <summary>
        /// AutoMapper configuration for this mapepr
        /// </summary>
        private readonly IMapper mMapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CityMapper()
        {
            // Configure the AutoMapper 
            mMapper = new MapperConfiguration(config =>
            {
                // Create CityWeather to CityWeatherContext map
                config.CreateMap<CityWeather, CityWeatherContext>()
                      // For weather data, deserialize json string
                      .ForMember(dest => dest.Weather, opt => opt.ResolveUsing(entity => JsonConvert.DeserializeObject<Dictionary<APIProviderType, WeatherInformationAPIModel>>(entity.WeatherData)));
            })
            // And create it afterwards
            .CreateMapper();
        }

        #endregion

        #region Mapping Methods

        /// <summary>
        /// Maps a <see cref="CityWeather"/> to a <see cref="CityWeatherContext"/> object
        /// </summary>
        /// <param name="context">The <see cref="CityWeather"/> to map</param>
        /// <returns><see cref="CityWeatherContext"/></returns>
        public CityWeatherContext Map(CityWeather entity) => mMapper.Map<CityWeatherContext>(entity);

        #endregion
    }
}
