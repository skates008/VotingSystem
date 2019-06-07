using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

using VotingSiteAPI.Services;


namespace VotingSiteAPI.CustomAuthFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OwinAuthorize : AuthorizationFilterAttribute
    {
        private readonly IWebConfigContainer _webConfigContainer;

        public OwinAuthorize()
        {
            // TODO: Is there a way to get injection working? Probably if I do this like I did with NetGCS, but is it really necessary? -SKF 6/5/19
            //_webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _webConfigContainer = new WebConfigContainer(new WebConfigReaderService());
        }

        /// <summary>
        /// Override to Web API filter method to handle our custom Auth check
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader?.Scheme == null || authHeader.Parameter == null)
            {
                actionContext.Response = actionContext.ControllerContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request.");

                return;
            }

            if (!IsTokenValid(authHeader))
            {
                // No authorization header has been supplied, therefore we are definitely not authorized
                // so return a 401 unauthorized result.
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, 
                    "Unauthorized.");
            }
        }

        private bool IsTokenValid(AuthenticationHeaderValue authValues)
        {
            return authValues.Scheme.Equals(_webConfigContainer.AuthScheme) && authValues.Parameter.Equals(_webConfigContainer.ApiKey);
        }
    }
}