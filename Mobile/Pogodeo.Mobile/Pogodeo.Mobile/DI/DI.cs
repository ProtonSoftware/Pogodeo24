using Dna;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Dependency Injection container for this application
    /// </summary>
    public static class DI
    {
        #region Public Shortcuts

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the current implementation of <see cref="ICityWeatherRepository"/>
        /// </summary>
        public static ICityWeatherRepository CityWeatherRepository => Framework.Service<ICityWeatherRepository>();

        /// <summary>
        /// A shortcut to access the <see cref="CityMapper"/>
        /// </summary>
        public static CityMapper CityMapper => Framework.Service<CityMapper>();

        /// <summary>
        /// A shortcut to access the <see cref="UIManager"/>
        /// </summary>
        public static IUIManager UI => Framework.Service<IUIManager>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the DI and binds initial view models to that
        /// </summary>
        public static void InitialSetup() => Framework.Construct<DefaultFrameworkConstruction>()
                                                      .AddFileLogger()
                                                      .AddPogodeoViewModels()
                                                      .AddDbContext()
                                                      .Build();

        #endregion
    }
}
