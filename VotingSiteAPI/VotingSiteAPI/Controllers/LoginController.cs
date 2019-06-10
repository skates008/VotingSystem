using System;
using System.Web;
using System.Web.Http;
using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Data.Enums;
using VotingSiteAPI.Domain.Models;
using VotingSiteAPI.Services;
using VotingSiteAPI.SharedModels;

//using NLog;


namespace VotingSiteAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// This controller handles user login, as well as retrieval of the text
    /// that will be displayed on the login page.
    /// </summary>
    [RoutePrefix("api/v1/login")]
    public class LoginController : ApiController
    {
        private readonly IWebConfigContainer _webConfigContainer;
        private readonly ILoginServices _loginServices;
        //private readonly ILogger<LoginController> _logger;

        //public TodoController(ILogger<TodoController> logger)
        //{
        //    _logger = logger;
        //}

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:VotingSiteAPI.Controllers.LoginController" /> class.
        /// </summary>
        /// <param name="webConfigContainer">The web configuration container.</param>
        /// <param name="loginServices">The login services.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// webConfigContainer
        /// or
        /// loginServices
        /// </exception>
        public LoginController(
            ///// <param name="logger">
            ///// ILogger&lt;LoginController&gt;
            ///// </param>
            /* ILogger<LoginController> logger, */
            IWebConfigContainer webConfigContainer,
            ILoginServices loginServices)
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _loginServices = loginServices ?? throw new ArgumentNullException(nameof(loginServices));
        }

        /// <summary>
        /// GET .../api/v1/login/pageData/{electionId}
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="RetrievedPageDataModel"/> class.
        /// </returns>
        [OwinAuthorize]
        [Route("pageData/{electionId}")]
        [HttpGet]
        public IHttpActionResult GetPageData(int electionId)
        {
            RetrievedPageDataModel result;

            try
            {
                result = _loginServices.GetPreLoginElectionData(electionId);
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(result);
        }

        /// <summary>
        /// <para>
        /// THIS is the POST "Login" method.
        /// </para>
        /// Validates whether (returns true) or not (returns false) the user 
        /// credentials passed to this method match what's in the database.
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>
        /// A boolean value indicating whether (true) or not (false) the user 
        /// credentials passed to this method match what's in the database. 
        /// </returns>
        [OwinAuthorize]
        [Route("")] // in other words, just www.theSite.com/api/v1/login
        [HttpPost]
        public IHttpActionResult UserCredentialsAreValid(
            UserCredentialsModel userCredentials)
        {
            bool result;

            try
            {
                result = _loginServices.ValidateUserCredentials(userCredentials);
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(result);
        }

        /// <summary>
        /// Verifies the passed User Credentials for the IVR system.
        /// </summary>
        /// <param name="ivrUserCredentialsInput">
        /// An instance of the <see cref="IvrUserCredentialsInputModel"/>
        /// class, which should be hydrated with the ElectionId, 'PIN' & 'SSN'
        /// </param>
        /// <returns>
        /// An instance of the <see cref="IvrUserLoginResponseModel"/> class
        /// who's <c>AuthResult</c> property will contain the <c>VoterId</c>
        /// of the Voter, if the voter specified by the parameter to this
        /// method is found. Else <c>AuthResult</c>
        /// </returns>
        // ReSharper disable once StringLiteralTypo
        [OwinAuthorize]
        [Route("ivrverifyuser")]
        [HttpPost]
        public IHttpActionResult VerifyIvrUserCredentials(
            IvrUserCredentialsInputModel ivrUserCredentialsInput)
        {
            // AuthResult will contain... See the values in the
            // IvrLoginStatusCodes enum.
            IvrUserLoginResponseModel loginWithLoggingResult = null;

            var browserAgent = Request.Headers.UserAgent.ToString();
            var usersIpAddress = HttpContext.Current.Request.UserHostAddress;

            try
            {
                loginWithLoggingResult = _loginServices.OrchestrateIvrUserLoginAndAttempt(
                    ivrUserCredentialsInput,
                    browserAgent,
                    usersIpAddress);

                //if (loginWithLoggingResult.AuthResult == (int) IvrLoginStatusCodes.AlreadyVoted)
                //{ }
            }
            catch (Exception oEx)
            {
                // TODO: actual logging would go here, at least.

                return InternalServerError(oEx);
            }

            return Ok(loginWithLoggingResult);
        }
    }
}
