using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHost : MasterDetailPage
    {
        #region Bindable Properties

        /// <summary>
        /// The current page to show in the page host
        /// </summary>
        public ApplicationPage CurrentPage
        {
            get { return (ApplicationPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty CurrentPageProperty =
            BindableProperty.Create(nameof(CurrentPage), typeof(ApplicationPage), typeof(PageHost), ApplicationPage.ProvideData, propertyChanged: CurrentPagePropertyChanged);

        /// <summary>
        /// The current view model to set on the page
        /// </summary>
        public BaseViewModel CurrentPageViewModel
        {
            get { return (BaseViewModel)GetValue(CurrentPageViewModelProperty); }
            set { SetValue(CurrentPageViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPageViewModel.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty CurrentPageViewModelProperty =
            BindableProperty.Create(nameof(CurrentPageViewModel), typeof(BaseViewModel), typeof(PageHost), null);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PageHost()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
        }

        #endregion

        #region Property Changed Events

        /// <summary>
        /// Fired when <see cref="CurrentPage"/> changes
        /// </summary>
        /// <param name="bindable">This page host as an object</param>
        /// <param name="oldValue">Old value of current page</param>
        /// <param name="newValue">New value of current page</param>
        private static async void CurrentPagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Get page values
            var newPage = (ApplicationPage)newValue;
            var oldPage = (ApplicationPage)oldValue;

            // Get current view model to set on that page
            var viewModel = (BaseViewModel)bindable.GetValue(CurrentPageViewModelProperty);

            // If current page didn't change
            if (newPage == oldPage)
            {
                // Just update the view model
                (bindable as PageHost).Detail.BindingContext = viewModel;
                return;
            }

            // Otherwise, change the page to specified one
            (bindable as PageHost).Detail = new NavigationPage(newPage.ToApplicationPage(viewModel));

            // Hide the menu
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);
            (bindable as PageHost).IsPresented = false;
        }

        #endregion
    }
}