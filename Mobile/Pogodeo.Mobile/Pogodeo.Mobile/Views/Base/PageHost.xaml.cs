using System;
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
            BindableProperty.Create(nameof(CurrentPageViewModel), typeof(BaseViewModel), typeof(PageHost), null, propertyChanged: CurrentPageViewModelPropertyChanged);

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
            // Get new page value
            var newPage = (ApplicationPage)newValue;

            // Get current view model to set on that page
            var viewModel = (BaseViewModel)bindable.GetValue(CurrentPageViewModelProperty);

            // Hide the menu
            DI.UI.HideMenu();

            // Android is stupidly optimized, wait a bit
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(225);

            // Change current page to specified one
            (bindable as PageHost).Detail = new NavigationPage(newPage.ToApplicationPage(viewModel));
        }

        /// <summary>
        /// Fired when <see cref="CurrentPageViewModel"/> changes
        /// </summary>
        /// <param name="bindable">This page host as an object</param>
        /// <param name="oldValue">Old value of current view model</param>
        /// <param name="newValue">New value of current view model</param>
        private static async void CurrentPageViewModelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Get new value
            var newViewModel = (BaseViewModel)newValue;

            // Get current page
            var currentPage = (ApplicationPage)bindable.GetValue(CurrentPageProperty);

            // Hide the menu
            DI.UI.HideMenu();
            
            // Android is stupidly optimized, wait a bit
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(225);

            // Change the page to the same one with new view model
            (bindable as PageHost).Detail = new NavigationPage(currentPage.ToApplicationPage(newViewModel));
        }

        #endregion
    }
}