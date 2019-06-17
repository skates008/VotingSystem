using System;
using System.Security.Claims;
using System.Threading.Tasks;

using VotingSite.UiDependentModels;
using VotingSiteAPI.SharedModels;


namespace VotingSite.UiDependentServices
{
    public interface IUiDependentLoginServices
    {
        Task<UserLoginResponseModel> OrchestrateVoterLoginAsync(UserCredentialsModel userCredentials);

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
        Task<LoginViewModel> GetLoginScreenDataAsync(int electionId);

        LandingPgViewModel BuildLandingPgViewModel(LoginViewModel loginViewModel);

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
        /// A hydrated instance of the <see cref="ClaimsIdentity"/> class.
        /// </returns>
        ClaimsIdentity CreateOwinUserIdentity(
            LoginViewModel loginVm,
            bool isPersistent = false);

        /// <summary>
        /// Checks and sets as needed the VotingIsOpen member of the
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
        LoginViewModel VotingIsOpenVerification(
            LoginViewModel loginViewModel,
            DateTime? dateTimeNow = null);
    }
}
