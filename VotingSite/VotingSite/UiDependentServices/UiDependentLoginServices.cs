using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using VotingSite.DAL;
using VotingSite.Domain;
using VotingSite.UiDependentModels;

using VotingSiteAPI.SharedModels;

using AutoMapper;


namespace VotingSite.UiDependentServices
{
    public class UiDependentLoginServices : IUiDependentLoginServices
    {
        private readonly ILoginScreenDataAccess _loginScreenDataAccess;

        public UiDependentLoginServices(
            ILoginScreenDataAccess loginScreenDataAccess)
        {
            _loginScreenDataAccess = loginScreenDataAccess ??
                                     throw new ArgumentNullException(nameof(loginScreenDataAccess));
        }


        public async Task<UserLoginResponseModel> OrchestrateVoterLoginAsync(UserCredentialsModel userCredentials)
        {
            //var userCredentialsValid = await
            //    _userCredentialsValidation.ValidateUserCredentialsAsync(userCredentials);

            //async Task<UserLoginResponseModel> LoginWebsiteUserAsync(
            //    UserCredentialsModel userCredentialsModel)
            var loginResponse = await _loginScreenDataAccess.LoginWebsiteUserAsync(userCredentials);



            //if (!userCredentialsValid)
            //{
            //    string errorMessage= "Invalid login attempt. Please try again.";
            //    ModelState.AddModelError(string.Empty, errorMessage);
            //    loginVm = _uiDependentLoginServices.VotingIsOpenVerification(loginVm);

            //    return Json(new { model = loginVm, error = errorMessage });
            //}




            return loginResponse;
        }


        /// <inheritdoc />
        /// <summary>
        /// Creates the owin user identity based on the credentials they enter.
        /// </summary>
        /// <param name="loginVm">
        /// The login vm.
        /// </param>
        /// <param name="isPersistent">
        /// If set to <c>true</c> [is persistent].
        /// </param>
        /// <returns>
        /// A hydrated instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class.
        /// </returns>
        public ClaimsIdentity CreateOwinUserIdentity(
            LoginViewModel loginVm, 
            bool isPersistent = false)
        {
            // SKF 12-June-2019 Might want to do Roles at some point
            // string[] userRoles = (string[])Session["UserRoles"];

            var claims = new List<Claim>
            {
                // create required claims
                new Claim(ClaimTypes.NameIdentifier, loginVm.LoginId),
                
                // probably shouldn't put both their username AND password
                // into the cookie, so just putting their username twice. -SKF 6/14/19
                new Claim(ClaimTypes.Name, loginVm.LoginId),

                new Claim(ClaimTypes.Actor, loginVm.BrowserAgent),
                new Claim("userIp", loginVm.UserIp)
            };

            // SKF 12-June-2019 Might want to do Roles at some point
            //userRoles.ToList().ForEach((role) => identity.AddClaim(new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");

            return identity;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gathers the data needed to display the login page.
        /// </summary>
        /// <param name="electionId">
        /// An integer containing the ElectionId.
        /// </param>
        /// <returns>
        /// A hydrated instance of the <see cref="LoginViewModel" /> class,
        /// within a Task. (Task&lt;LoginViewModel&gt;)
        /// </returns>
        public async Task<LoginViewModel> GetLoginScreenDataAsync(int electionId)
        {
            LoginViewData retrievedLoginData = await _loginScreenDataAccess.GetDataForLoginScreenAsync(electionId);

            var dtNow = DateTime.Now;
            if (dtNow >= retrievedLoginData.OpenDate && dtNow <= retrievedLoginData.CloseDate)
            {
                retrievedLoginData.VotingIsOpen = true;
            }

            // Map LoginViewData -> LoginViewModel
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<LoginViewData, LoginViewModel>(); });
            var iMapper = mapperConfig.CreateMapper();
            var loginViewModel = iMapper.Map<LoginViewData, LoginViewModel>(retrievedLoginData);

            return loginViewModel;
        }

        /// <summary>
        /// Builds the landing pg view model.
        /// </summary>
        /// <param name="loginViewModel">
        /// An instance of the <see cref="LoginViewModel"/> class.
        /// </param>
        /// <returns>
        /// A hydrated instance of the <see cref="LandingPgViewModel"/>
        /// </returns>
        public LandingPgViewModel BuildLandingPgViewModel(LoginViewModel loginViewModel)
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<LoginViewModel, LandingPgViewModel>(); });
            var iMapper = mapperConfig.CreateMapper();
            var landingPgViewModel = iMapper.Map<LoginViewModel, LandingPgViewModel>(loginViewModel);

            return landingPgViewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// Checks and sets as needed the <c>VotingIsOpen</c> member of the
        /// <see cref="LoginViewModel" />.
        /// </summary>
        /// <param name="loginViewModel">
        /// The login view model.
        /// </param>
        /// <param name="dateTimeNow">
        /// A <see cref="DateTime"/> containing the date and time that is
        /// considered 'Now.'
        /// <para>
        /// Defaults to null. [and the method will create its own DateTime.Now]
        /// </para>
        /// </param>
        /// <returns>
        /// The same instance of the <c>LoginViewModel</c> passed to this
        /// method with the VotingIsOpen member in a known state.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public LoginViewModel VotingIsOpenVerification(
            LoginViewModel loginViewModel,
            DateTime? dateTimeNow = null)
        {
            var now = dateTimeNow ?? DateTime.Now;
            loginViewModel.VotingIsOpen = now >= loginViewModel.OpenDate && now <= loginViewModel.CloseDate;

            return loginViewModel;
        }
    }
}
