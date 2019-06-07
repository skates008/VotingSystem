
using System;

using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Domain.Entities;


namespace VotingSiteAPI.Data.Repositories
{
    public interface IElectionsRepository : IRepository<Election>
    {
        /// <summary>
        /// Gets an instance of the database factory, which provides access
        /// to the <c>DbContext</c>.
        /// </summary>
        IDatabaseFactory DbFactory { get; }
    }


    /// <summary>
    /// This is the Repository pattern implementation for the Elections table.
    /// </summary>
    /// <seealso cref="VotingSiteAPI.Data.Infrastructure.RepositoryBase{Election}" />
    /// <seealso cref="VotingSiteAPI.Data.Repositories.IElectionsRepository" />
    public class ElectionsRepository : RepositoryBase<Election>, IElectionsRepository
    {
        private readonly IDatabaseFactory _dbFactory;

        public ElectionsRepository(IDatabaseFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the database factory, which provides access
        /// to the <c>DbContext</c>.
        /// </summary>
        public IDatabaseFactory DbFactory => this.DatabaseFactory;
    }
}
