using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Manages all the UI stuff in this application
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Shows specified page on top of the current navigation page
        /// </summary>
        /// <param name="page">The page to show as a modal</param>
        public async Task ShowModalOnCurrentNavigation(ContentPage page)
        {
            // Get current navigation page
            var navigation = (App.Current.MainPage as PageHost).Detail.Navigation;

            // Push a page on top of that
            await navigation.PushAsync(page, true);

            // Hide the menu afterwards
            HideMenu();
        }

        /// <summary>
        /// Hides the menu on demand
        /// </summary>
        public void HideMenu() => (App.Current.MainPage as PageHost).IsPresented = false;
    }
}
