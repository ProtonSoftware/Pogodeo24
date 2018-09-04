using Pogodeo.Core;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for single weather card containing informations from single external API
    /// </summary>
    public class WeatherCardViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The name of external API provider
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The weather icon to show in this card
        /// </summary>
        public WeatherIconType Icon { get; set; }

        /// <summary>
        /// The value of temperature to show
        /// </summary>
        public int ValueTemperature { get; set; }

        /// <summary>
        /// The value of rain to show
        /// </summary>
        public int ValueRain { get; set; }

        /// <summary>
        /// The value of humidity to show
        /// </summary>
        public int ValueHumidity { get; set; }

        /// <summary>
        /// The value of wind speed to show
        /// </summary>
        public int ValueWind { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of a provider that returned this data</param>
        /// <param name="updateModel">The initial data to set on this card</param>
        public WeatherCardViewModel(string name, CardDataAPIModel updateModel)
        {
            // Set the name
            Name = name;

            // Update this view model's data
            UpdateData(updateModel);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates this view model with new data
        /// </summary>
        /// <param name="updateModel">New data</param>
        public void UpdateData(CardDataAPIModel updateModel)
        {
            // Update every value
            ValueTemperature = updateModel.ValueTemperature;
            ValueRain = updateModel.ValueRain;
            ValueHumidity = updateModel.ValueHumidity;
            ValueWind = updateModel.ValueWind;
            Icon = updateModel.WeatherIcon;
        }

        #endregion
    }
}
