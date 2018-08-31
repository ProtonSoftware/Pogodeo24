namespace Pogodeo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Error
    {
        #region Public Properties

        public string Classification { get; set; }
        public string Identification { get; set; }
        public string Message { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Classification, Identification, Message);
        }
    }
}
