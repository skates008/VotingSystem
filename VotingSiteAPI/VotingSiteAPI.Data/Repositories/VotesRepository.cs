
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Data.Repositories
{
    public interface IVotesRepository : IRepository<Vote>
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <returns>
        /// IVotingSiteAPIDbCtx
        /// </returns>
        IVotingSiteAPIDbCtx GetDbContext();
    }

    public class VotesRepository : RepositoryBase<Vote>, IVotesRepository
    {
        private readonly IVotingSiteAPIDbCtx _dbContext;

        public VotesRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Get();
        }

        public IVotingSiteAPIDbCtx GetDbContext() => _dbContext;
    }
}
