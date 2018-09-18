namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for main application state
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
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
        /// And sets the initial view model for that page if provided
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model to set initially on page (if provided)</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Change current page to specified one
            CurrentPage = page;

            // Set new credentials in page host
            (App.Current.MainPage as PageHost).CurrentPageViewModel = viewModel;
            (App.Current.MainPage as PageHost).CurrentPage = page;
        }

        #endregion
    }
}
