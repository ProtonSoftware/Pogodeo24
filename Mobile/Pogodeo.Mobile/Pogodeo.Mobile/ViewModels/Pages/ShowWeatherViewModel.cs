using Dna;
using Pogodeo.Core;
using Pogodeo.Core.Localization;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        /// The view model for date control that is displayed on this page
        /// </summary>
        public DateTitleViewModel DateViewModel { get; set; } = new DateTitleViewModel();

        /// <summary>
        /// The list of External Api cards that contain informations about weather
        /// </summary>
        public ObservableCollection<WeatherCardViewModel> Items { get; set; } = new ObservableCollection<WeatherCardViewModel>();

        #endregion

        #region Commands
        
        /// <summary>
        /// The command to show a page where user can add new place
        /// </summary>
        public ICommand AddNewPlaceCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowWeatherViewModel(string city)
        {
            // Create commands
            AddNewPlaceCommand = new RelayCommand(async () => await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ProvideDataPage()), true));

            // Get name from previous page
            CityName = city;

            // Set page's title
            Title = string.Format(LocalizationResources.ShowWeatherTitle, CityName);
            
            // Get weather data
            Task.Run(GetWeatherData)
                // And update the view afterwards
                .ContinueWith((s) => UpdateWeatherCards());

            Items.Add(new WeatherCardViewModel("Onet", new CardHourDataAPIModel { ValueTemperature = 27, ValueHumidity = 65, ValueRain = 23, ValueWind = 9, WeatherIcon = WeatherIconType.Sun }));
            Items.Add(new WeatherCardViewModel("Interia", new CardHourDataAPIModel { ValueTemperature = 23, ValueHumidity = 55, ValueRain = 22, ValueWind = 11, WeatherIcon = WeatherIconType.Rain }));
            Items.Add(new WeatherCardViewModel("WP", new CardHourDataAPIModel { ValueTemperature = 21, ValueHumidity = 75, ValueRain = 21, ValueWind = 7, WeatherIcon = WeatherIconType.Cloud }));
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Tries to get weather data from database
        /// Sends an API request and collects the data if not found
        /// </summary>
        private async Task GetWeatherData()
        {
            // Get our saved data
            var savedData = DI.CityWeatherRepository.GetCityWeather(CityName);

            // If we have one...
            if (savedData != null && savedData.Weather != null)
            {
                // Send an API request that checks if we have up-to-date data
                var apiDateResult = await WebRequests.PostAsync<bool>(GetAPIRoute(ApiRoutes.CheckIfWeatherRequiresUpdate), CityName);

                // If we got a response...
                if (apiDateResult != null && apiDateResult.Successful)
                {
                    // If we got true response, we have to update our data
                    // Otherwise...
                    if (!apiDateResult.ServerResponse)
                        // Our data is up-to-date, don't request anything
                        return;
                }
                // Otherwise...
                else
                    // TODO: Show that we got no data back
                    return;
            }

            // Send an API request that gets us weather data
            var apiWeatherResult = await WebRequests.PostAsync<APIWeatherResponse>(GetAPIRoute(ApiRoutes.GetWeatherForCity), CityName);

            // If we got a data back...
            if (apiWeatherResult != null && apiWeatherResult.Successful && apiWeatherResult.ServerResponse != null)
            {
                // Deserialize json to suitable model
                var apiResponse = apiWeatherResult.ServerResponse;

                // Cache the data to database
                DI.CityWeatherRepository.SaveWeatherForCity(CityName, apiResponse.WeatherResponses);                
            }
            // Otherwise...
            else
                // TODO: Show that we got no data back
                return;
        }

        /// <summary>
        /// Gets the url for API call
        /// </summary>
        /// <param name="path">The additional path that is added to the host</param>
        /// <returns>URL</returns>
        private string GetAPIRoute(string path) => "http://pogodeo24.pl" + path;

        /// <summary>
        /// Fired when date has changed and weather info needs an update
        /// </summary>
        private void UpdateWeatherCards()
        {
            // TODO: Get current date
            var currentDate = new DateTime(2018, 11, 20, 15, 00, 00, 00);

            // TODO: Logic
            //var currentWeatherData = APIResponse.WeatherResponses[APIProviderType.AccuWeather].TodayWeatherTruncatedData.TryGetValue(currentDate, out var weatherInfo);

            // Update every card
            foreach (var card in Items)
            {
                // Update the card with current external API info
                //card.UpdateData(weatherInfo);
            }
        }

        #endregion
    }
}
