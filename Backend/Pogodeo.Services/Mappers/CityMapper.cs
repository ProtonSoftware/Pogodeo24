using AutoMapper;
using Pogodeo.DataAccess;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
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
                config.CreateMap<BigCity, BigCityContext>();
                config.CreateMap<SmallCity, SmallCityContext>();
            })
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

        #endregion
    }
}
