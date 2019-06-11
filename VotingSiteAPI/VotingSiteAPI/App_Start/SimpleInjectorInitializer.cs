using System.Web.Http;

using VotingSiteAPI.Data;
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Services;

using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;


namespace VotingSiteAPI
{
    /// <summary>
    /// Initializes all the key stuff needed for use of Simple Injector.
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
            var container = GetInitializeContainer();

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

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
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // These are here for possible later use. -SKF
            //// Go look in all assemblies and register all implementations
            //// of ICommandHandler<T> by their closed interface:
            //container.Register(
            //    typeof(ICommandHandler<>),
            //    AppDomain.CurrentDomain.GetAssemblies());

            //// Decorate each returned ICommandHandler<T> object with
            //// a TransactionCommandHandlerDecorator<T>.
            //container.RegisterDecorator(
            //    typeof(ICommandHandler<>),
            //    typeof(TransactionCommandHandlerDecorator<>));

            //// Decorate each returned ICommandHandler<T> object with
            //// a DeadlockRetryCommandHandlerDecorator<T>.
            //container.RegisterDecorator(
            //    typeof(ICommandHandler<>),
            //    typeof(DeadlockRetryCommandHandlerDecorator<>));

            //// Decorate handlers conditionally with validation. In
            //// this case based on their metadata.
            //container.RegisterDecorator(
            //    typeof(ICommandHandler<>),
            //    typeof(ValidationCommandHandlerDecorator<>),
            //    c => ContainsValidationAttributes(c.ServiceType));

            //// Decorates all handlers with an authorization decorator.
            //container.RegisterDecorator(
            //    typeof(ICommandHandler<>),
            //    typeof(AuthorizationCommandHandlerDecorator<>));
            // --]

            // Register the types for later injection 
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);

            //container.Register<IModelHydrator<WebDefRemReasonsModel>, WebDefRemReasonsHydrator>(Lifestyle.Scoped);

            container.Register<IWebConfigContainer, WebConfigContainer>(Lifestyle.Scoped);
            container.Register<IWebConfigReaderService, WebConfigReaderService>(Lifestyle.Scoped);

            container.Register<ILoginAttemptsRepository, LoginAttemptsRepository>(Lifestyle.Scoped);

            container.Register<IElectionsServices, ElectionsServices>(Lifestyle.Scoped);
            container.Register<IElectionsRepository, ElectionsRepository>(Lifestyle.Scoped);

            container.Register<ILoginServices, LoginServices>(Lifestyle.Scoped);

            container.Register<IVotesRepository, VotesRepository>(Lifestyle.Scoped);
            container.Register<IVotesServices, VotesServices>(Lifestyle.Scoped);

            container.Register<IVotersRepository, VotersRepository>(Lifestyle.Scoped);
            container.Register<IVotersServices, VotersServices>(Lifestyle.Scoped);

            container.Register<IContestsRepository, ContestsRepository>(Lifestyle.Scoped);
            container.Register<ILandingPageServices, LandingPageServices>(Lifestyle.Scoped);

            container.Register<IContestServices, ContestServices>(Lifestyle.Scoped);

            container.Register<ICandidatesServices, CandidatesServices>(Lifestyle.Scoped);
            container.Register<ICandidatesRepository, CandidatesRepository>(Lifestyle.Scoped);

            // ----

            // ****** registration of the dbContext is HERE ******
            // ...and must be done like this because the generated dbContext
            // has FIVE public constructors.
            container.Register<IVotingSiteAPIDbCtx>(() => new VotingSiteAPIDbCtx(), Lifestyle.Scoped);

            // Don't need this at this time as the construction of the
            // DbContext (above) handles retrieval of the connection string.
            //// carries around the database connection string so that it's not
            //// hard-coded all over the place.
            //container.Register<IConnStrContainer>(
            //    () => new ConnStrContainer("VotingSiteConnStr"), Lifestyle.Singleton);

            return container;
        }
    }
}
