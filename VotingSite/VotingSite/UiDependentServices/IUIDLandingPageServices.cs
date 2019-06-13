using System.Threading.Tasks;
using VotingSite.UiDependentModels;

namespace VotingSite.UiDependentServices
{
    public interface IUIDLandingPageServices
    {
        Task<LandingPgViewModel> GetLandingPageDataAsync(int electionId);
    }
}