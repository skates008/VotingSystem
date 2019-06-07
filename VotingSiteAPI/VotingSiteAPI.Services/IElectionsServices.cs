
using VotingSiteAPI.Domain.Models;


namespace VotingSiteAPI.Services
{
    public interface IElectionsServices
    {
        ElectionIdOpenAndClosed GetElectionIdOpenAndClosed(string callingAddress);
    }
}
