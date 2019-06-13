using System.Threading.Tasks;
using VotingSite.Domain;

namespace VotingSite.DAL
{
    public interface ILandingPageDataAccess
    {
        Task<LandingPageViewData> GetLandingPageViewData(int electionId);
    }
}