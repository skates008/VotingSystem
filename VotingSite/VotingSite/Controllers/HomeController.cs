using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.Owin.Security;

using VotingSite.DataAccessServices;
using VotingSite.UiDependentModels;
using VotingSite.UiDependentServices;

using VotingSiteAPI.SharedModels;


namespace VotingSite.Controllers
{
    public class HomeController : AsyncController
    {
        private readonly IUiDependentLoginServices _uiDependentLoginServices;
        private readonly int _currentElectionId;

        public HomeController(
            IWebConfigContainer webConfigContainer,
            IUiDependentLoginServices uiDepLoginServices)
        {
            var configContainer = webConfigContainer ??
                                throw new ArgumentNullException(nameof(webConfigContainer));
            _currentElectionId = configContainer.ElectionId;

            //if (_currentElectionId <= 0)
            //{
            // TODO: How should we handle this condition? -- Per Ken; log it! ..and... put up a notice about "come back in a hour" or something.
            //    // the read failed!
            //}

            _uiDependentLoginServices =
                uiDepLoginServices ?? throw new ArgumentNullException(nameof(uiDepLoginServices));
        }

        /// <summary>
        /// Loads the <see cref="LoginViewModel"/> instance by calling the
        /// <c>UiDependentLoginServices.GetLoginScreenDataAsync(int electionId)</c> method.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="ActionResult"/> class.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var usersHost = Request.UserAgent;
            var usersIpAddress = Request.UserHostAddress;

            LoginViewModel loginVm = await _uiDependentLoginServices.GetLoginScreenDataAsync(_currentElectionId);

            loginVm.UserIp = usersIpAddress;
            loginVm.BrowserAgent = usersHost;

            return View("Index2", loginVm);
        }

        /////// <summary>
        /////// NOTE: THIS IS THE ORIGINAL Razor View/Pages code. The new ng view
        /////// is called by the <c>ngIndex</c> method below.
        /////// TODO: comment out or actually remove this once the ng-based login screen is solid.
        /////// </summary>
        /////// <param name="loginVm">The login vm.</param>
        /////// <param name="returnUrl">The return URL.</param>
        /////// <returns></returns>
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////[ActionName("Index")]
        ////public async Task<ActionResult> IndexPost(LoginViewModel loginVm, string returnUrl)
        ////{
        ////    throw new Exception("This method is deprecated.");
        ////}

        [HttpPost]
        [ActionName("ngIndex")]
        public async Task<ActionResult> Index(LoginViewModel loginVm, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                // go back and try again
                loginVm = _uiDependentLoginServices.VotingIsOpenVerification(loginVm);

                var errorMessage = "Invalid ViewState.";
                return Json(new {model = loginVm, error = errorMessage});
            }

            var clientIpAddress = Request.UserHostAddress;
            var userAgent = Request.UserAgent;

            var userCredentials = new UserCredentialsModel
            {
                ElectionId = Convert.ToInt32(loginVm.ElectionId),
                UsernameOrId = loginVm.LoginId,
                PasswordOrPin = loginVm.LoginPin,
                UserAgent = userAgent,
                UserIpAddress = clientIpAddress
            };

            // Call the API and let it do most of the heavy lifting.
            var loginAttemptResult = await _uiDependentLoginServices.OrchestrateVoterLoginAsync(userCredentials);

            if (!loginAttemptResult.LoginSuccessful)
            {
                var errorMessage = loginAttemptResult.ErrorInformation;

                return Json(new { model = loginVm, error = errorMessage });
            }

            await LogoutAsync();

            ClaimsIdentity identity = _uiDependentLoginServices.CreateOwinUserIdentity(loginVm);
            AuthenticationManager.SignIn(identity);

            return await Task.Run<ActionResult>(() => RedirectToAction("Index", "Landing"));
        }

        public async Task<ActionResult> LogoutAsync()
        {
            // logout functionality 
            AuthenticationManager.SignOut("ApplicationCookie", "ExternalCookie");

            return await Task.Run<ActionResult>(() => RedirectToAction("Index"));
        }

        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>
        /// The authentication manager.
        /// </value>
        private IAuthenticationManager AuthenticationManager => System.Web.HttpContext.Current.GetOwinContext().Authentication;

    }
}