
using System.Net.Http;


namespace VotingSite.DataAccessServices.HttpClientHelpers
{
    public interface IHttpClientProvider
    {
        /// <summary>
        /// Gets the HTTP client instance.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="HttpClient"/> class.
        /// </returns>
        HttpClient GetHttpClientInstance();
    }
}
