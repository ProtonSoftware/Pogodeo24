using System;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for date title control that displays current date in nice and animated format
    /// </summary>
    public class DateTitleViewModel : BaseViewModel
    {
        #region Public Properties

        public string Day { get; set; }
        public string Hour { get; set; }

        /// <summary>
        /// The date of today
        /// </summary>
        public DateTime TodayDate => DateTime.Today;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fired whenever the value of the slider changes
        /// </summary>
        /// <param name="newValue">The new value of slider</param>
        public void SliderValueChanged(double newValue)
        {
            // Cast new value to integer
            var newIntValue = (int)newValue;

            // Based on it, pick new date values
            switch (newIntValue / 3)
            {
                case 0:
                    Day = TodayDate.Day + "." + TodayDate.Month;
                    break;
                case 1:
                    Day = TodayDate.AddDays(1).Day + "." + TodayDate.AddDays(1).Month;
                    break;
                case 2:
                    Day = TodayDate.AddDays(2).Day + "." + TodayDate.AddDays(2).Month;
                    break;
                case 3:
                    Day = TodayDate.AddDays(3).Day + "." + TodayDate.AddDays(3).Month;
                    break;
                case 4:
                    Day = TodayDate.AddDays(4).Day + "." + TodayDate.AddDays(4).Month;
                    break;
                case 5:
                    Day = TodayDate.AddDays(5).Day + "." + TodayDate.AddDays(5).Month;
                    break;
                case 6:
                    Day = TodayDate.AddDays(6).Day + "." + TodayDate.AddDays(6).Month;
                    break;
            }

            switch (newIntValue % 3)
            {
                case 0:
                    Hour = "Rano";
                    break;
                case 1:
                    Hour = "Popoludnie";
                    break;
                case 2:
                    Hour = "Noc";
                    break;
            }
        }

        #endregion
    }
}
