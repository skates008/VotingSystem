
namespace VotingSiteAPI.Services
{
    public interface IWebConfigContainer
    {
        string ApiKey { get; }
        string AuthScheme { get; }
    }
}
