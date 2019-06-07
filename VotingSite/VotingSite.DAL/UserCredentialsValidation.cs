using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using VotingSite.DataAccessServices;
using VotingSite.DataAccessServices.HttpClientHelpers;
using VotingSiteAPI.SharedModels;

using Newtonsoft.Json;


// in this project, Classes wrap calls to the VotingSiteAPI [solution]

namespace VotingSite.DAL
{
    public class UserCredentialsValidation : IUserCredentialsValidation
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IWebConfigContainer _webConfigContainer;

        public UserCredentialsValidation(
            IWebConfigContainer webConfigContainer,
            IHttpClientProvider httpClientProvider)
        {
            _webConfigContainer =
                webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _httpClientProvider =
                httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        /// <inheritdoc />
        /// <summary>
        /// Checks the credentials entered by the user against the database.
        /// <para>
        /// Calls "https://*/api/v1/login/{userCredentialsModel}" (See: API's
        /// LoginController)
        /// </para>
        /// </summary>
        /// <param name="userCredentialsModel">
        /// An instance of the <see cref="UserCredentialsModel"/> class
        /// containing the username/password entered by the user.
        /// </param>
        /// <returns>
        /// A Task&lt;bool&gt; that indicates whether (true) or not (false)
        /// the credentials entered by the user are valid.
        /// </returns>
        public async Task<bool> ValidateUserCredentialsAsync(
            UserCredentialsModel userCredentialsModel)
        {
            const string exceptionHeadText =
                "EXCEPTION in: Task<LoginViewModel> ValidateUserCredentialsAsync(int electionId) ***\r\n";

            var httpClient = _httpClientProvider.GetHttpClientInstance();

            try
            {
                var callUrl = _webConfigContainer.BaseApiUrl;
                if (!callUrl.EndsWith("/"))
                {
                    callUrl = callUrl.TrimEnd() + "/";
                }

                callUrl += "login/";

                var myContent = JsonConvert.SerializeObject(userCredentialsModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // build the Authorization header value
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        _webConfigContainer.AuthScheme,
                        _webConfigContainer.ApiKey);

                HttpResponseMessage response = await httpClient.PostAsync(callUrl, byteContent);

                bool callResponse = false;
                if (response.IsSuccessStatusCode && (response.StatusCode == HttpStatusCode.OK))
                {
                    //var testReturnValue = response.Content.
                    var data = await response.Content.ReadAsStringAsync();
                    callResponse = JsonConvert.DeserializeObject<bool>(data);
                }
                else
                {
                    throw new Exception("The attempted call of the API apparently failed. (in ValidateUserCredentialsAsync())");
                }

                return callResponse;
            }
            catch (HttpRequestException httpReqException)
            {
                // TODO: Add real logging
                Debug.WriteLine(
                    exceptionHeadText +
                    httpReqException.Message + "\r\n" +
                    httpReqException.StackTrace);
                throw;
            }
            catch (Exception oEx)
            {
                // TODO: Add real logging
                Debug.WriteLine(
                    exceptionHeadText +
                    oEx.Message + "\r\n" + 
                    oEx.StackTrace);
                throw;
            }
        }

    }
}
