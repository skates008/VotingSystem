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


// NOT CURRENTLY USED. -SKF 6/14/19
// in this project, Classes wrap calls to the VotingSiteAPI [solution]

namespace VotingSite.DAL
{
    public class UserCredentialsValidation : IUserCredentialsValidation
    {
        //private readonly IHttpClientProvider _httpClientProvider;
        //private readonly IWebConfigContainer _webConfigContainer;

        public UserCredentialsValidation()
            //IWebConfigContainer webConfigContainer,
            //IHttpClientProvider httpClientProvider)
        {
            //_webConfigContainer =
            //    webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            //_httpClientProvider =
            //    httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        // NOT CURRENTLY USED. -SKF 6/14/19

    }
}
