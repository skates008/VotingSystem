
using System;


namespace VotingSite.DataAccessServices
{
    public class WebConfigContainer : IWebConfigContainer
    {
        private readonly IWebConfigReaderService _webConfigReaderSvc;
        private string _apiKey;
        private DateTime _apiKeyExpiration;
        private string _authScheme;
        private DateTime _authSchemeExpiration;
        private int _electionId;
        //private DateTime _electionIdExpiration; -- this value is going to be pretty darn static, so I don't think caching is necessary -SKF 6/5/19
        private string _baseApiUrl;
        private DateTime _baseApiUrlExpiration;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebConfigContainer"/> class.
        /// </summary>
        /// <param name="webConfigReaderSvc">The web configuration reader SVC.</param>
        /// <exception cref="ArgumentNullException">webConfigReaderSvc</exception>
        public WebConfigContainer(
            IWebConfigReaderService webConfigReaderSvc)
        {
            _webConfigReaderSvc = webConfigReaderSvc ?? throw new ArgumentNullException(nameof(webConfigReaderSvc));

            _electionId = -1;
        }

        /// <summary>
        /// Gets the value of the &lt;appSettings&gt; element "apiKey".
        /// </summary>
        public string ApiKey
        {
            get
            {
                if (!CacheIsExpiredOrEmptyForItem(_apiKey, _apiKeyExpiration))
                {
                    return _apiKey;
                }

                _apiKey = _webConfigReaderSvc.GetAppSetting<string>("apiKey");

                // TODO: Probably want to add the 'timeCached' (or in this case, CacheApiKeyFor as they may want/need to be that granular) to the web.config as well.
                _apiKeyExpiration = DateTime.Now.AddMinutes(30);

                return _apiKey;
            }
        }

        /// <summary>
        /// Gets the value of the &lt;appSettings&gt; element "authScheme".
        /// </summary>
        public string AuthScheme
        {
            get
            {
                if (!CacheIsExpiredOrEmptyForItem(_authScheme, _authSchemeExpiration))
                {
                    return _authScheme;
                }

                _authScheme = _webConfigReaderSvc.GetAppSetting<string>("authScheme");

                // TODO: Probably want to add the 'timeCached' (or in this case, CacheAuthSchemeFor (DateTime or Timespan) as they may want/need to be that granular) to the web.config as well.
                _authSchemeExpiration = DateTime.Now.AddMinutes(60);

                return _authScheme;
            }
        }

        //<add key = "CurrentElectionId" value="1" />
        //<add key = "BaseApiUrl" value="http://localhost:63190/api/v1/" />

        /// <summary>
        /// Gets the election identifier ("CurrentElectionId") from the
        /// Web.config
        /// </summary>
        public int ElectionId
        {
            get
            {
                if (_electionId > 0)
                {
                    return _electionId;
                }

                _electionId = _webConfigReaderSvc.GetAppSetting<int>("CurrentElectionId");

                return _electionId;
            }
        }

        /// <summary>
        /// Gets the base API URL.
        /// </summary>
        public string BaseApiUrl
        {
            get
            {
                // https://calpers.com/api/v1/ .Length == 28, so 20's totally fair
                if (DateTime.Now < _baseApiUrlExpiration &&
                    !string.IsNullOrWhiteSpace(_baseApiUrl) &&
                    (_baseApiUrl.Length > 20))
                {
                    return _baseApiUrl;
                }

                _baseApiUrl = _webConfigReaderSvc.GetAppSetting<string>("BaseApiUrl");

                // TODO: Probably want to add the 'timeCached' (or in this case, CacheBaseApiUrlFor (DateTime or Timespan) as they may want/need to be that granular) to the web.config as well.
                _baseApiUrlExpiration = DateTime.Now.AddHours(3);

                return _baseApiUrl;
            }
        }

        /// <summary>
        /// Caches the is expired or empty for item.
        /// </summary>
        /// <param name="configValue">
        /// The value retrieved from the Web.config file.
        /// </param>
        /// <param name="expirationDateTime">
        /// The expiration date time. This is the date & time at which the
        /// value associated with 'this' expiration DateTime expires, which
        /// means that the <paramref name="configValue"/> will be [re-]read
        /// from the Web.config file.
        /// </param>
        /// <returns>
        /// A boolean value indicating whether (true) or not (false) the
        /// value in the <paramref name="configValue"/> parameter is either
        /// 'Null or whitespace' or expired and thus needs to be [re-]read
        /// from the Web.config file.
        /// </returns>
        private bool CacheIsExpiredOrEmptyForItem(
            string configValue,
            DateTime expirationDateTime)
        {
            return (DateTime.Now > expirationDateTime || string.IsNullOrWhiteSpace(configValue));
        }
    }
}
