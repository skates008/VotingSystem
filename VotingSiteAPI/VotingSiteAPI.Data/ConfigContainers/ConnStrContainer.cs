
using System;

using System.Configuration;


namespace VotingSiteAPI.Data.ConfigContainers
{
    public class ConnStrContainer : IConnStrContainer
    {
        /// <inheritdoc />
        /// <summary>
        /// Wraps a named connection string and can be dependency injected down
        /// the graph.
        /// </summary>
        /// <param name="connStringName">
        /// The connection string name.
        /// </param>
        /// <exception cref="ArgumentNullException">connectionStringNameStr</exception>
        public ConnStrContainer(string connStringName)
        {
            ConnectionStringName = connStringName ??
                throw new ArgumentNullException(nameof(connStringName));
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the name of the connection string associated with this
        /// instance of the <see cref="T:RamcoDcApis.ConfigContainers.ConnStrContainer" /> class.
        /// </summary>
        public string ConnectionStringName { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets a named connection string from this application's Web or App
        /// .config file.
        /// </summary>
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }
    }
}
