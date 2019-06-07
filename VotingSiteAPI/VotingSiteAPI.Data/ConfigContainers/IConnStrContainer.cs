
namespace VotingSiteAPI.Data.ConfigContainers
{
    /// <summary>
    /// This is the interface / "Contract" for the 
    /// <see cref="ConnStrContainer"/> class.
    /// </summary>
    public interface IConnStrContainer
    {
        /// <summary>
        /// Returns the name of the connection string associated with this
        /// instance of the <see cref="T:ConfigContainers.ConnStrContainer" /> class.
        /// </summary>
        string ConnectionStringName { get; }

        /// <summary>
        /// Gets a named connection string from this application's Web or App
        /// .config file.
        /// </summary>
        string GetConnectionString();
    }
}
