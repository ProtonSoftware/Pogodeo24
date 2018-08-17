using System.ComponentModel.DataAnnotations;

namespace Pogodeo
{
    /// <summary>
    /// The view model for data in initial provide data page
    /// Used to submit a form
    /// </summary>
    public class ProvideDataViewModel
    {
        /// <summary>
        /// The name of a city user has provided in the input
        /// </summary>
        [Required]
        public string CityName { get; set; }
    }
}
