using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VotingSite.Controllers;


namespace VotingSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorInitializer.Initialize();

            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier =
                System.Security.Claims.ClaimTypes.NameIdentifier;

            // typical stuff below this
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_End()
        {
            // log out
            Debug.WriteLine("****** MADE IT TO: Session_End()");

            // TODO: if I want to auto-logout the user, I'll need an instance of the HomeController, or to move the logout code to something I can get to from here. -SKF
            //HomeController hc = new HomeController();
        }
    }
}
