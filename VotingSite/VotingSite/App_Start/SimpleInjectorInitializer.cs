using System.Reflection;
using System.Web.Mvc;

using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

using VotingSite.DataAccessServices;
using VotingSite.DataAccessServices.HttpClientHelpers;
using VotingSite.DAL;
using VotingSite.Services;
using VotingSite.UiDependentServices;


namespace VotingSite
{
    /// <summary>
    /// Initializes all the key stuff needed for use of the Simple Injector
    /// Dependency Injection container.
    /// </summary>
    public static class SimpleInjectorInitializer
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>
        /// An instance of the Simple Injector <see cref="Container"/> class.
        /// </returns>
        public static Container Initialize()
        {
            // Create the container as usual.
            var container = GetInitializeContainer();

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            //InitializeMapper.Initialize();

            return container;
        }

        /// <summary>
        /// Registration of injected objects happens in here.
        /// </summary>
        /// <returns>
        /// An instance of the Simple Injector <see cref="Container"/> class.
        /// </returns>
        private static Container GetInitializeContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register the types for later injection 
            //container.Register<IModelHydrator<CustWebDefRemReasonsModel>, CustWebDefRemReasonsHydrator>(Lifestyle.Scoped);

            container.Register<IUIDLandingPageServices, UIDLandingPageServices>(Lifestyle.Scoped);
            container.Register<ILandingPageDataAccess, LandingPageDataAccess>(Lifestyle.Scoped);

            container.Register<IUiDependentLoginServices, UiDependentLoginServices>(Lifestyle.Scoped);
            container.Register<IUserCredentialsValidation, UserCredentialsValidation>(Lifestyle.Scoped);
            container.Register<ILoginScreenDataAccess, LoginScreenDataAccess>(Lifestyle.Scoped);

            // not sure if I need this or not. -SKF
            //container.Register<IBase64Codec, Base64Codec>(Lifestyle.Scoped);

            container.Register<IWebConfigContainer, WebConfigContainer>(Lifestyle.Scoped);

            var webConfigReader = new WebConfigReaderService();
            container.Register<IWebConfigReaderService>(() => webConfigReader, Lifestyle.Scoped);

            var clientProvider = new HttpClientProvider(webConfigReader);
            // Lifestyle.Singleton ?
            container.Register<IHttpClientProvider>(() => clientProvider, Lifestyle.Scoped);


            //container.Register<IElectionsRepository, ElectionsRepository>(Lifestyle.Scoped);
            container.Register<ILoginServices, LoginServices>(Lifestyle.Scoped);

            // ----



            return container;
        }
    }
}
