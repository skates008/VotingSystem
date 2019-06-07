
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Data.Repositories
{
    public interface IVotersRepository : IRepository<Voter>
    { }

    public class VotersRepository : RepositoryBase<Voter>, IVotersRepository
    {
        public VotersRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        { }
    }
}
