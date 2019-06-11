
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Data.Repositories
{
    public interface ILoginAttemptsRepository : IRepository<LoginAttempt>
    { }


    public class LoginAttemptsRepository : RepositoryBase<LoginAttempt>, ILoginAttemptsRepository
    {
        public LoginAttemptsRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        { }
    }
}
