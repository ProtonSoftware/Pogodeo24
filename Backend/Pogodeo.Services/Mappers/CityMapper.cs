using AutoMapper;
using Newtonsoft.Json;
using Pogodeo.Core;
using Pogodeo.DataAccess;
using System;
using System.Collections.Generic;

namespace Pogodeo.Services
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
                // Map from BigCity to BigCityContext
                config.CreateMap<BigCity, BigCityContext>()
                      .ForMember(dest => dest.AccuWeatherContext, opt => opt.ResolveUsing(entity => new WeatherContext
                      {
                          LastUpdateDate = entity.AccuWeatherWeather == null ? new DateTime() : entity.AccuWeatherWeather.LastUpdateDate,
                          TodayWeatherTruncatedData = JsonConvert.DeserializeObject<Dictionary<DateTime, CardHourDataAPIModel>>(entity.AccuWeatherWeather == null ? "" : entity.AccuWeatherWeather.WeatherHourData),
                          NextDaysWeatherTruncatedData = JsonConvert.DeserializeObject<Dictionary<DateTime, CardDayDataAPIModel>>(entity.AccuWeatherWeather == null ? "" : entity.AccuWeatherWeather.WeatherDayData)
                      }))
                      .ForMember(dest => dest.AerisWeatherContext, opt => opt.ResolveUsing(entity => new WeatherContext
                      {
                          LastUpdateDate = entity.AerisWeatherWeather == null ? new DateTime() : entity.AerisWeatherWeather.LastUpdateDate,
                          TodayWeatherTruncatedData = JsonConvert.DeserializeObject<Dictionary<DateTime, CardHourDataAPIModel>>(entity.AerisWeatherWeather == null ? "" : entity.AerisWeatherWeather.WeatherHourData),
                          NextDaysWeatherTruncatedData = JsonConvert.DeserializeObject<Dictionary<DateTime, CardDayDataAPIModel>>(entity.AerisWeatherWeather == null ? "" : entity.AerisWeatherWeather.WeatherDayData)
                      }))
                      .ForMember(dest => dest.WWOContext, opt => opt.ResolveUsing(entity => new WeatherContext
                      {
                          LastUpdateDate = entity.WWOWeather == null ? new DateTime() : entity.WWOWeather.LastUpdateDate,
                          TodayWeatherTruncatedData = JsonConvert.DeserializeObject<Dictionary<DateTime, CardHourDataAPIModel>>(entity.WWOWeather == null ? "" : entity.WWOWeather.WeatherHourData),
                          NextDaysWeatherTruncatedData = JsonConvert.DeserializeObject<Dictionary<DateTime, CardDayDataAPIModel>>(entity.WWOWeather == null ? "" : entity.WWOWeather.WeatherDayData)
                      }));

                // Map from SmallCity to SmallCityContext
                config.CreateMap<SmallCity, SmallCityContext>();

                // Map from WeatherContext to WeatherInformationAPIModel
                config.CreateMap<WeatherContext, WeatherInformationAPIModel>()
                      .ReverseMap();
            })
            // And create it afterwards
            .CreateMapper();
        }

        #endregion

        #region Mapping Methods

        /// <summary>
        /// Maps a <see cref="BigCity"/> to a <see cref="BigCityContext"/> object
        /// </summary>
        /// <param name="city">The <see cref="BigCity"/> to map</param>
        /// <returns><see cref="BigCityContext"/></returns>
        public BigCityContext Map(BigCity city) => mMapper.Map<BigCityContext>(city);

        /// <summary>
        /// Maps a <see cref="SmallCity"/> to a <see cref="SmallCityContext"/> object
        /// </summary>
        /// <param name="city">The <see cref="SmallCity"/> to map</param>
        /// <returns><see cref="SmallCityContext"/></returns>
        public SmallCityContext Map(SmallCity city) => mMapper.Map<SmallCityContext>(city);

        /// <summary>
        /// Maps a <see cref="WeatherContext"/> to a <see cref="WeatherInformationAPIModel"/> object
        /// </summary>
        /// <param name="context">The <see cref="WeatherContext"/> to map</param>
        /// <returns><see cref="WeatherInformationAPIModel"/></returns>
        public WeatherInformationAPIModel Map(WeatherContext context) => mMapper.Map<WeatherInformationAPIModel>(context);

        /// <summary>
        /// Maps a <see cref="WeatherInformationAPIModel"/> to a <see cref="WeatherContext"/> object
        /// </summary>
        /// <param name="weather">The <see cref="WeatherInformationAPIModel"/> to map</param>
        /// <returns><see cref="WeatherContext"/></returns>
        public WeatherContext Map(WeatherInformationAPIModel weather) => mMapper.Map<WeatherContext>(weather);

        #endregion
    }
}
