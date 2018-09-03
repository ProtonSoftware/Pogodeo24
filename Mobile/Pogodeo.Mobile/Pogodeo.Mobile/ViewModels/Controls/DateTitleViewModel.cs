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

            switch (newIntValue / 3)
            {
                case 0:
                    Day = "01.09";
                    break;
                case 1:
                    Day = "02.09";
                    break;
                case 2:
                    Day = "03.09";
                    break;
                case 3:
                    Day = "04.09";
                    break;
                case 4:
                    Day = "05.09";
                    break;
                case 5:
                    Day = "06.09";
                    break;
                case 6:
                    Day = "07.09";
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
