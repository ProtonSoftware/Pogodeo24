using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pogodeo.Mobile
{
    /// <summary>
    /// Main entry point for this application
    /// </summary>
    public partial class App : Application
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public App()
        {
            // Do default thing
            InitializeComponent();

            // Setup our Dependency Injection for this application
            DI.InitialSetup();

            // Setup main page to host our pages
            MainPage = new PageHost();
        }

        #endregion

        #region Lifecycle Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion
    }
}
