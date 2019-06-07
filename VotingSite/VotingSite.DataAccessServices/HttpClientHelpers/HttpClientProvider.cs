
using System;
using System.Net.Http;


namespace VotingSite.DataAccessServices.HttpClientHelpers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        // I read some time ago that you really want to use a single instance
        // of this class, thus holding it as a static in this class. -SKF
        private static HttpClient _httpClient;

        /// <summary>
        /// Holds an instance of the <see cref="WebConfigReaderService"/> class.
        /// </summary>
        private readonly IWebConfigReaderService _webCfgRdr;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientProvider"/> class.
        /// </summary>
        public HttpClientProvider(
            IWebConfigReaderService webConfigReader)
        {
            _webCfgRdr = webConfigReader ?? throw new ArgumentNullException(nameof(webConfigReader));

            InitializeApiHttpClient();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the HTTP client instance.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="T:System.Net.Http.HttpClient" /> class.
        /// </returns>
        public HttpClient GetHttpClientInstance()
        {
            return _httpClient;
        }

        /// <summary>
        /// Initializes the configured HTTP client.
        /// </summary>
        private void InitializeApiHttpClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_webCfgRdr.GetAppSetting<string>("BaseApiUrl"))
            };

            //// clear the default headers
            //_httpClient.DefaultRequestHeaders.Accept.Clear();

            //// TODO: (?) Read the accept header value from the Web.config file
            //_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        }

    }
}
