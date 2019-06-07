
using System;


namespace VotingSiteAPI.Services
{
    /// <summary>
    /// This class wraps access to the &lt;appSettings&gt; elements.
    /// </summary>
    public class WebConfigContainer : IWebConfigContainer
    {
        private readonly IWebConfigReaderService _webConfigReaderSvc;
        private string _apiKey;
        private DateTime _apiKeyExpiration;
        private string _authScheme;
        private DateTime _authSchemeExpiration;

        public WebConfigContainer(
            IWebConfigReaderService webConfigReaderSvc)
        {
            _webConfigReaderSvc = webConfigReaderSvc ?? throw new ArgumentNullException(nameof(webConfigReaderSvc));
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

                // TODO: Probably want to add the 'timeCached' (or in this case, CacheAuthSchemeFor as they may want/need to be that granular) to the web.config as well.
                _authSchemeExpiration = DateTime.Now.AddMinutes(60);

                return _authScheme;
            }
        }

        /// <summary>
        /// Caches the is expired or empty for item.
        /// </summary>
        /// <param name="configKey">The configuration key.</param>
        /// <param name="expirationDateTime">The expiration date time.</param>
        /// <returns></returns>
        private bool CacheIsExpiredOrEmptyForItem(
            string configKey,
            DateTime expirationDateTime)
        {
            return (DateTime.Now > expirationDateTime || string.IsNullOrWhiteSpace(configKey));
        }

    }
}
