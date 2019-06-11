using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.Owin.Security;

using VotingSite.DataAccessServices;
using VotingSite.DAL;
using VotingSite.Services;
using VotingSite.UiDependentModels;
using VotingSite.UiDependentServices;
using VotingSiteAPI.SharedModels;


namespace VotingSite.Controllers
{
    public class HomeController : AsyncController
    {
        private readonly ILoginServices _loginServices;
        private readonly IUiDependentLoginServices _uiDependentLoginServices;
        private readonly int _currentElectionId;
        private readonly IUserCredentialsValidation _userCredentialsValidation;

        public HomeController(
            IWebConfigReaderService webConfigReaderService,
            ILoginServices loginServices,
            IUiDependentLoginServices uiDepLoginServices,
            IUserCredentialsValidation userCredentialsValidation)
        {
            var readerService = webConfigReaderService ??
                                throw new ArgumentNullException(nameof(webConfigReaderService));
            _loginServices = loginServices ?? throw new ArgumentNullException(nameof(loginServices));
            _uiDependentLoginServices =
                uiDepLoginServices ?? throw new ArgumentNullException(nameof(uiDepLoginServices));
            _userCredentialsValidation = userCredentialsValidation ??
                                         throw new ArgumentNullException(nameof(userCredentialsValidation));

            _currentElectionId = readerService.GetAppSetting<int>("CurrentElectionId");

            //if (_currentElectionId <= 0)
            //{
            // TODO: How should we handle this condition? -- log it! ..and... put up a notice about "come back in a hour" or something.
            //    // the read failed!
            //}
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

            if (Request.QueryString["ng"] != null)
            return View("Index2", loginVm);

            return View(loginVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public async Task<ActionResult> IndexPost(LoginViewModel loginVm, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                // go back and try again
                loginVm = _uiDependentLoginServices.VotingIsOpenVerification(loginVm);
                return View(loginVm);
            }

            var userCredentials = new UserCredentialsModel
            {
                ElectionId = Convert.ToInt32(loginVm.ElectionId),
                UsernameOrId = loginVm.LoginId,
                PasswordOrPin = loginVm.LoginPin
            };

            // DAL needed here.
            var userCredentialsValid = await 
                _userCredentialsValidation.ValidateUserCredentialsAsync(userCredentials);

            if (!userCredentialsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please try again.");
                loginVm = _uiDependentLoginServices.VotingIsOpenVerification(loginVm);

                return View(loginVm);
            }

            AuthenticationManager.SignOut("ApplicationCookie", "ExternalCookie");

            ClaimsIdentity identity = _uiDependentLoginServices.CreateOwinUserIdentity(loginVm);
            
            // TODO: Do I need to add a role that all voter's can belong to?
            // because I thought this signed me in, and yet the [Authorize] on the Landing Controller's Index method is sending me back to the login page.
            
            AuthenticationManager.SignIn(identity);

            // TODO: The UserIp & BrowserAgent fields are used when logging 'this' login attempt to the LoginAttempts table
            //var landingPgVm = _uiDependentLoginServices.BuildLandingPgViewModel(loginVm);

            return await Task.Run<ActionResult>(() =>
                RedirectToAction("Index", "Landing"));

            // return View() -- there is no opportunity to give the controller name
            // which means if I want to go from here, I do have to redirect.
        }

        [HttpPost]
        [ActionName("ngIndex")]
        public async Task<ActionResult> ngIndexPost(LoginViewModel loginVm, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                // go back and try again
                loginVm = _uiDependentLoginServices.VotingIsOpenVerification(loginVm);
                return View(loginVm);
            }

            var userCredentials = new UserCredentialsModel
            {
                ElectionId = Convert.ToInt32(loginVm.ElectionId),
                UsernameOrId = loginVm.LoginId,
                PasswordOrPin = loginVm.LoginPin
            };

            // DAL needed here.
            var userCredentialsValid = await
                _userCredentialsValidation.ValidateUserCredentialsAsync(userCredentials);

            if (!userCredentialsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please try again.");
                loginVm = _uiDependentLoginServices.VotingIsOpenVerification(loginVm);

                return Json(loginVm);
            }

            AuthenticationManager.SignOut("ApplicationCookie", "ExternalCookie");

            ClaimsIdentity identity = _uiDependentLoginServices.CreateOwinUserIdentity(loginVm);

            AuthenticationManager.SignIn(identity);

            return await Task.Run<ActionResult>(() =>
                RedirectToAction("Index", "Landing"));
        }

        public async Task<ActionResult> Logout()
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