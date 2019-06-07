
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Services
{
    public interface ILoginServices
    {
        RetrievedPageDataModel GetPreLoginElectionData(int electionId);

        bool ValidateUserCredentials(UserCredentialsModel userCredentials);
    }
}
