using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Manages every UI interactions
    /// </summary>
    public interface IUIManager
    {
        Task ShowModalOnCurrentNavigation(ContentPage page);
        void HideMenu();
    }
}
