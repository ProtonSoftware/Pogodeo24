using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Base page for every page in this application to inherit from
    /// </summary>
    public class BasePage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Animate("x", (s) => Layout(new Rectangle(((1 - s) * Width), Y, Width, Height)), 16, 600, Easing.Linear);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.Animate("x", (s) => Layout(new Rectangle((s * Width) * -1, Y, Width, Height)), 16, 600, Easing.Linear);
        }
    }
}
