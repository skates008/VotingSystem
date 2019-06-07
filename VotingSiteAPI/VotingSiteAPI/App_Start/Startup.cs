using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;

using VotingSiteAPI.App_Start;


[assembly: OwinStartup("Startup", typeof(Startup))]
namespace VotingSiteAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            /*var webApiConfiguration = */
            ConfigureWebApi(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            //var config = new HttpConfiguration();

            // use config in something, let alone the 'app' parameter
        }
    }
}
