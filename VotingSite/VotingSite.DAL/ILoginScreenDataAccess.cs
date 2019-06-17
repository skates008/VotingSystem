
using System.Threading.Tasks;

using VotingSite.Domain;
using VotingSiteAPI.SharedModels;


namespace VotingSite.DAL
{
    public interface ILoginScreenDataAccess
    {
        /// <summary>
        /// Gets the data for needed for display on the login screen.
        /// </summary>
        /// <param name="electionId">The election identifier.</param>
        /// <returns>
        /// A hydrated instance of the <see cref="LoginViewData"/> class,
        /// wrapped in a Task&lt;T&gt;.
        /// </returns>
        Task<LoginViewData> GetDataForLoginScreenAsync(int electionId);

        /// <summary>
        /// Checks the credentials entered by the user against the database.
        /// <para>
        /// Calls "https://*/api/v1/login/{userCredentialsModel}" (See: API's
        /// LoginController)
        /// </para>
        /// </summary>
        /// <param name="userCredentialsModel">
        /// An instance of the <see cref="UserCredentialsModel"/> class
        /// containing the username/password entered by the user.
        /// </param>
        /// <returns>
        /// A Task&lt;UserLoginResponseModel&gt; that indicates whether or not
        /// the credentials entered by the user are valid, along with all the
        /// other things that have to happen to log someone into this system.
        /// </returns>
        Task<UserLoginResponseModel> LoginWebsiteUserAsync(UserCredentialsModel userCredentialsModel);
    }
}
