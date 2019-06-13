
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VotingSite.DataAccessServices;
using VotingSite.DataAccessServices.HttpClientHelpers;
using VotingSite.Domain;


namespace VotingSite.DAL
{
    public class LandingPageDataAccess : ILandingPageDataAccess
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IWebConfigContainer _webConfigContainer;

        public LandingPageDataAccess(
            IWebConfigContainer webConfigContainer,
            IHttpClientProvider httpClientProvider)
        {
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));

            _httpClientProvider =
                httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }


        public async Task<LandingPageViewData> GetLandingPageViewData(int electionId)
        {
            var httpClient = _httpClientProvider.GetHttpClientInstance();
            var exceptionMsgString =
                $"EXCEPTION in: Task<LandingPageViewData> GetLandingPageViewData(int electionId ({electionId})) ***\r\n";

            try
            {
                var callUrl = _webConfigContainer.BaseApiUrl;

                // contests?electionId=1
                // api/v1/contests/{electionId}
                callUrl += $"contests/{electionId}";

                // build / add the Authorization header value
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        _webConfigContainer.AuthScheme,
                        _webConfigContainer.ApiKey);

                HttpResponseMessage response = await httpClient.GetAsync(callUrl);

                LandingPageViewData landingPgViewData;
                if (response.IsSuccessStatusCode && (response.StatusCode == HttpStatusCode.OK))
                {
                    var data = await response.Content.ReadAsStringAsync();

                    landingPgViewData = JsonConvert.DeserializeObject<LandingPageViewData>(data);
                }
                else
                {
                    throw new Exception("The attempted API call apparently failed. (in GetLandingPageViewData())");
                }

                return landingPgViewData ?? new LandingPageViewData();
            }
            catch (HttpRequestException httpReqException)
            {
                // TODO: Add real logging
                Debug.WriteLine(
                    exceptionMsgString +
                    httpReqException.Message + "\r\n" +
                    httpReqException.StackTrace);
                throw;
            }
            catch (Exception oEx)
            {
                // TODO: Add real logging
                Debug.WriteLine(
                    exceptionMsgString +
                    oEx.Message + "\r\n" +
                    oEx.StackTrace);
                throw;
            }
        }

    }
}
