
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;


[assembly: OwinStartup(typeof(VotingSite.App_Start.Startup))]
namespace VotingSite.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Home/Index"),
                //LogoutPath = new PathString("/account/LogOff"),
                CookieName = ".khVotingSite",
                SlidingExpiration = true,
                AuthenticationType = "ApplicationCookie",
                AuthenticationMode = AuthenticationMode.Active
            });

            // OWIN has its own version of an authentication manager in the
            // IAuthenticationManager interface which is attached to the
            // HttpContext object. To get a reference to it you can use:
            // HttpContext.GetOwinContext().Authentication;

        }

    }


}
