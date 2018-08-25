namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for main application state
    /// </summary>
    public class ApplicationViewModel
    {
        #region Public Properties

        /// <summary>
        /// Indicates which page is shown currently in the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates the application to specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPage page)
        {
            // Change current page to specified one
            CurrentPage = page;

            App.Current.MainPage = page.ToApplicationPage();
        }

        #endregion
    }
}
