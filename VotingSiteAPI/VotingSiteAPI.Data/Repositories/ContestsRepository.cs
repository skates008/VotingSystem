
using System.Collections.Generic;
using System.Linq;

using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Data.Repositories
{
    public interface IContestsRepository : IRepository<Contest>
    {
        IEnumerable<Contest> GetContestsByElectionId(int electionId);
    }

    public class ContestsRepository : RepositoryBase<Contest>, IContestsRepository
    {
        private readonly IVotingSiteAPIDbCtx _dbContext;

        public ContestsRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Get();
        }

        // Specialized Data Access goes here [below]. -SKF

        /// <summary>
        /// Gets all the Contests for a given election based on the Election IDs
        /// </summary>
        /// <param name="electionId">
        /// An integer containing the <c>ElectionId</c> for which its contests
        /// must be retrieved.
        /// </param>
        /// <returns>
        /// IEnumerable&lt;Contest&gt;
        /// </returns>
        public IEnumerable<Contest> GetContestsByElectionId(
            int electionId)
        {
            // Don't need proxies when explicitly loading.
            _dbContext.Configuration.ProxyCreationEnabled = false;

            // Contests -> BallotTypeMapping -> BallotType -> Elections
            IEnumerable<Contest> retrievedContests =
                (from contest in _dbContext.Contests
                 join btm in _dbContext.BallotTypeMappings on contest.BallotTypeId equals btm.BallotTypeId
                 join bt in _dbContext.BallotTypes on btm.BallotTypeId equals bt.Id
                 join e in _dbContext.Elections on bt.Id equals e.Id
                 where e.Id == electionId
                 select contest);

            return retrievedContests;
        }



    }
}
