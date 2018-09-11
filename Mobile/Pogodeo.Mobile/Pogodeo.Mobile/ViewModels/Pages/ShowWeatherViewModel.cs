using Dna;
using Pogodeo.Core;
using Pogodeo.Core.Localization;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for show weather page
    /// </summary>
    public class ShowWeatherViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The raw slider value as double number
        /// </summary>
        private double mSliderValue;

        /// <summary>
        /// Indicates if switch is toggled
        /// </summary>
        private bool mIsSwitchToggled;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of a city that came from user
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// The raw value of slider as double number
        /// Used for setting timestamps of shown weather info
        /// </summary>
        public double SliderValue
        {
            get => mSliderValue;
            set
            {
                // If value didn't changed
                if (value == mSliderValue)
                    // Don't do anything
                    return;

                // Otherwise, set the new value
                mSliderValue = value;

                // Call an event to change timestamps
                DateViewModel.SliderValueChanged(value);

                // Update weather cards with new info
                //UpdateWeatherCards();
            }
        }

        /// <summary>
        /// Indicates if switch is toggled
        /// </summary>
        public bool IsSwitchToggled
        {
            get => mIsSwitchToggled;
            set
            {
                mIsSwitchToggled = value;

                SliderValue = 0;
            }
        }

        /// <summary>
        /// Returns inverted value of switch toggle
        /// Used for swapping sliders
        /// </summary>
        public bool IsSwitchToggledInverted => !mIsSwitchToggled;

        /// <summary>
        /// The response from API that should be shown in this page
        /// </summary>
        public APIWeatherResponse APIResponse { get; set; }

        /// <summary>
        /// The view model for date control that is displayed on this page
        /// </summary>
        public DateTitleViewModel DateViewModel { get; set; } = new DateTitleViewModel();

        /// <summary>
        /// The list of External Api cards that contain informations about weather
        /// </summary>
        public ObservableCollection<WeatherCardViewModel> Items { get; set; } = new ObservableCollection<WeatherCardViewModel>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowWeatherViewModel(string city)
        {
            // Get name from previous page
            CityName = city;

            // Set page's title
            Title = string.Format(LocalizationResources.ShowWeatherTitle, CityName);

            // Send an API request
            Task.Run(GetAPIData);

            Items.Add(new WeatherCardViewModel("Onet", new CardHourDataAPIModel { ValueTemperature = 27, ValueHumidity = 65, ValueRain = 23, ValueWind = 9, WeatherIcon = WeatherIconType.Sun }));
            Items.Add(new WeatherCardViewModel("Interia", new CardHourDataAPIModel { ValueTemperature = 23, ValueHumidity = 55, ValueRain = 22, ValueWind = 11, WeatherIcon = WeatherIconType.Rain }));
            Items.Add(new WeatherCardViewModel("WP", new CardHourDataAPIModel { ValueTemperature = 21, ValueHumidity = 75, ValueRain = 21, ValueWind = 7, WeatherIcon = WeatherIconType.Cloud }));
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Sends an API request and collects the data
        /// </summary>
        private async Task GetAPIData()
        {
            // Send an API request
            var apiResult = await WebRequests.PostAsync<APIWeatherResponse>(GetAPIRoute(), CityName);

            // If we got a data back...
            if (apiResult != null && apiResult.Successful && apiResult.ServerResponse != null)
            {
                // Deserialize json to suitable view model
                APIResponse = apiResult.ServerResponse;

                // Update cards with that data
                UpdateWeatherCards();
            }
            // Otherwise...
            else
                // TODO: Show that we got no data back
                return;
        }

        /// <summary>
        /// Gets the url for API call
        /// </summary>
        /// <returns>URL</returns>
        private string GetAPIRoute() => "http://pogodeo24.pl" + ApiRoutes.GetWeatherForCity;

        /// <summary>
        /// Fired when date has changed and weather info needs an update
        /// </summary>
        private void UpdateWeatherCards()
        {
            // TODO: Get current date
            var currentDate = new DateTime(2018, 11, 20, 15, 00, 00, 00);

            // TODO: Logic
            var currentWeatherData = APIResponse.WeatherResponses[APIProviderType.AccuWeather].TodayWeatherTruncatedData.TryGetValue(currentDate, out var weatherInfo);

            // Update every card
            foreach (var card in Items)
            {
                // Update the card with current external API info
                card.UpdateData(weatherInfo);
            }
        }

        #endregion
    }
}
