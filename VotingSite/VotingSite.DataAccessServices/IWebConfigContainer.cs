
namespace VotingSite.DataAccessServices
{
    public interface IWebConfigContainer
    {
        string ApiKey { get; }

        string AuthScheme { get; }

        int ElectionId { get; }

        /// <summary>
        /// Gets the base API URL.
        /// <para>
        /// Guaranteed to have a final '/'
        /// </para>
        /// </summary>
        string BaseApiUrl { get; }
    }
}
