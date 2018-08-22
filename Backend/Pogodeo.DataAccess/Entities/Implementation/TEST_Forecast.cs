using System;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class TEST_Forecast : BaseObject<int>
    {
        #region Public Properties
        public DateTime EventDate { get; set; }
        public float TempMorning { get; set; }
        public float TempNoon { get; set; }
        public float TempEvening { get; set; }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TEST_Forecast()
        {
        
        }

        #endregion
    }
}
