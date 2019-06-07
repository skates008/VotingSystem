using System;
using System.Web.Http;
using VotingSiteAPI.CustomAuthFilter;
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
    [OwinAuthorize]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="webConfigContainer">The web configuration container.</param>
        /// <param name="loginServices">The login services.</param>
        /// <exception cref="ArgumentNullException">
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
        [Route("")] // in other words, just www.theSite.com/api/v1/login
        [HttpPost]
        public IHttpActionResult UserCredentialsAreValid(
            UserCredentialsModel userCredentials)
        {
            bool result;

            try
            {
                //// get header value(s)
                //var authValues = Request.Headers.Authorization;

                //if (authValues?.Scheme == null || authValues.Parameter == null)
                //{
                //    return BadRequest();
                //}

                //if (!authValues.Scheme.Equals(_webConfigContainer.AuthScheme) ||
                //    !authValues.Parameter.Equals(_webConfigContainer.ApiKey))
                //{
                //    return Unauthorized();
                //}

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
        /// <remarks>
        /// Since the Model Binder will auto-convert the parameter values for
        /// me, I've got them set as int, string, string.
        /// </remarks>
        /// <returns>
        /// An integer containing the <c>VoterId</c> of the Voter, if the
        /// voter specified by the parameters to this method is found.
        /// </returns>
        // ReSharper disable once StringLiteralTypo
        [Route("ivrverifyuser")]
        [HttpPost]
        public IHttpActionResult VerifyIvrUserCredentials(
            IvrUserCredentialsInputModel ivrUserCredentialsInput)
        {
            int voterId = 0;

            //result = _loginServices.ValidateUserCredentials(userCredentials);
            try
            {
                // TODO: check the number of login attempts first!

                // how this should be set in the client
                //httpClient.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", "Your_token");

                var ucm = new UserCredentialsModel
                {
                    UsernameOrId = ivrUserCredentialsInput.PIN,
                    PasswordOrPin = ivrUserCredentialsInput.SSN,
                    ElectionId = ivrUserCredentialsInput.ElectionID
                };

                _loginServices.ValidateUserCredentials(ucm);
                voterId = ucm.VoterId; 
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(voterId);
        }
    }
}
