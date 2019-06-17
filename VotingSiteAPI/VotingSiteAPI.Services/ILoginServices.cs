
using VotingSiteAPI.Domain.Models;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Services
{
    public interface ILoginServices
    {
        RetrievedPageDataModel GetPreLoginElectionData(int electionId);

        UserLoginResponseModel OrchestrateUserLogin(UserCredentialsModel userCredentials);

        bool ValidateUserCredentials(UserCredentialsModel userCredentials);

        /// <summary>
        /// Attempt to Log the user in, as well as logging the attempt.
        /// </summary>
        /// <param name="ivrUserCredentials">
        /// An instance of the user's credentials via the
        /// <see cref="IvrUserCredentialsInputModel"/> class.
        /// </param>
        /// <param name="browserAgent">
        /// A string containing... for the logging bit.
        /// </param>
        /// <param name="userIpAddress">
        /// A string containing... for the logging bit.
        /// </param>
        /// <returns>
        /// A hydrated instance of the <see cref="IvrUserLoginResponseModel"/>
        /// class.
        /// </returns>
        IvrUserLoginResponseModel OrchestrateIvrUserLoginAndAttempt(
            IvrUserCredentialsInputModel ivrUserCredentials,
            string browserAgent,
            string userIpAddress);

        bool? IsAccountLockedOut(UserCredentialsModel userCredentials);
    }
}
