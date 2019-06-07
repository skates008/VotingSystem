
using System.Threading.Tasks;

using VotingSite.Domain;


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
    }
}
