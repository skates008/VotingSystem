
using System.Threading.Tasks;

using VotingSiteAPI.SharedModels;


namespace VotingSite.DAL
{
    public interface IUserCredentialsValidation
    {
        /// <summary>
        /// Gets the data required to be displayed on the login screen,
        /// including field labels, etc.
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
        /// A boolean value indicating whether (true) 
        /// </returns>
        Task<bool> ValidateUserCredentialsAsync(
            UserCredentialsModel userCredentialsModel);
    }
}
