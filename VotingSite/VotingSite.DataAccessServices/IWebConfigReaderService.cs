
namespace VotingSite.DataAccessServices
{
    public interface IWebConfigReaderService
    {
        /// <summary>
        /// Gets the specified appSettings value.
        /// </summary>
        /// <typeparam name="T">
        /// The Type of value to return.
        /// </typeparam>
        /// <param name="keyName">
        /// A string containing the name of the appSettings key, who's value 
        /// is to be returned.
        /// </param>
        /// <returns>
        /// A value of Type T.
        /// </returns>
        T GetAppSetting<T>(string keyName);
    }
}
