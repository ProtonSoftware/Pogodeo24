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

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the DI and binds initial view models to that
        /// </summary>
        public static void InitialSetup() => Framework.Construct<DefaultFrameworkConstruction>()
                                                      .AddFileLogger()
                                                      .AddPogodeoViewModels()
                                                      .Build();

        #endregion
    }
}
