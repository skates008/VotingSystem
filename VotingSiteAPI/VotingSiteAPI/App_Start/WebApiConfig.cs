
using System.Web.Http;

using Newtonsoft.Json.Serialization;


namespace VotingSiteAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // The next several lines of code came from the following blog post.
            // https://developer.okta.com/blog/2018/09/07/build-simple-crud-with-aspnet-webapi-vue
            // (Just the ASP.Net Web API bits of course.)

            // Set JSON formatter as default one and remove XmlFormatter
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Remove the XML formatter -- Actually, going to leave it alone for now.
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            // END blog post code
        }
    }
}
