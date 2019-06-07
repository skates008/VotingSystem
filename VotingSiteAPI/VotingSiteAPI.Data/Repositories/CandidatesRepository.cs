
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Data.Repositories
{
    public interface ICandidatesRepository : IRepository<Candidate>
    { }

    /// <summary>
    /// Initializes an instance of this (<see cref="CandidatesRepository"/>)
    /// class.
    /// </summary>
    /// <seealso cref="VotingSiteAPI.Data.Infrastructure.RepositoryBase{Candidate}" />
    /// <seealso cref="VotingSiteAPI.Data.Repositories.ICandidatesRepository" />
    public class CandidatesRepository : RepositoryBase<Candidate>, ICandidatesRepository
    {
        public CandidatesRepository(
            IDatabaseFactory dbFactory) : base(dbFactory)
        { }
    }
}
