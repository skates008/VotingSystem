
namespace VotingSite.DataAccessServices
{
    public interface IWebConfigContainer
    {
        string ApiKey { get; }

        string AuthScheme { get; }

        int ElectionId { get; }

        string BaseApiUrl { get; }
    }
}
