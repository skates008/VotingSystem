using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using VotingSite.DataAccessServices;
using VotingSite.DataAccessServices.HttpClientHelpers;
using VotingSite.Domain;

using Newtonsoft.Json;


// in this project, Classes wrap calls to the VotingSiteAPI [solution]

namespace VotingSite.DAL
{
    /// <summary>
    /// This is like a "LoginRepository", only this code calls APIs.
    /// </summary>
    public class LoginScreenDataAccess : ILoginScreenDataAccess
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IWebConfigReaderService _webConfigReaderService;
        private readonly IWebConfigContainer _webConfigContainer;

        public LoginScreenDataAccess(
            IWebConfigReaderService webConfigReaderService,
            IWebConfigContainer webConfigContainer,
            IHttpClientProvider httpClientProvider)
        {
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));

            // TODO: Hopefully I can kill this because IWebConfigContainer is here now.
            _webConfigReaderService =
                webConfigReaderService ?? throw new ArgumentNullException(nameof(webConfigReaderService));

            _httpClientProvider = 
                httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the data required to be displayed on the login screen,
        /// including field labels, etc.
        /// <para>
        /// Calls "https://*/api/v1/login/pageData/{electionId}" (See: API's
        /// LoginController)
        /// </para>
        /// </summary>
        /// <param name="electionId">The election identifier.</param>
        /// <returns>
        /// A hydrated instance of the <see cref="LoginViewData"/> class.
        /// </returns>
        public async Task<LoginViewData> GetDataForLoginScreenAsync(int electionId)
        {
            var httpClient = _httpClientProvider.GetHttpClientInstance();

            try
            {
                var callUrl = //_webConfigReaderService.GetAppSetting<string>("BaseApiUrl");
                    _webConfigContainer.BaseApiUrl;

                if (!callUrl.EndsWith("/"))
                {
                    callUrl = callUrl.TrimEnd() + "/";
                }

                callUrl += $"login/pageData/{electionId}";

                // build the Authorization header value
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        _webConfigContainer.AuthScheme,
                        _webConfigContainer.ApiKey);

                HttpResponseMessage response = await httpClient.GetAsync(callUrl);

                LoginViewData loginViewData;
                if (response.IsSuccessStatusCode && (response.StatusCode == HttpStatusCode.OK))
                {
                    var data = await response.Content.ReadAsStringAsync();

                    loginViewData = JsonConvert.DeserializeObject<LoginViewData>(data);
                }
                else
                {
                    throw new Exception("The attempted API call apparently failed. (in GetDataForLoginScreenAsync())");
                }

                return loginViewData ?? new LoginViewData();
            }
            catch (HttpRequestException httpReqException)
            {
                // TODO: Add real logging
                Debug.WriteLine(
                    "EXCEPTION in: Task<LoginViewModel> GetDataForLoginScreenAsync(int electionId) ***\r\n" +
                    httpReqException.Message + "\r\n" +
                    httpReqException.StackTrace);
                throw;
            }
            catch (Exception oEx)
            {
                // TODO: Add real logging
                Debug.WriteLine(
                    "EXCEPTION in: Task<LoginViewModel> GetDataForLoginScreenAsync(int electionId) ***\r\n" +
                    oEx.Message + "\r\n" + 
                    oEx.StackTrace);
                throw;
            }
        }

    }
}
