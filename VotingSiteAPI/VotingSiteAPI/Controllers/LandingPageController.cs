using System;
using System.Web.Http;
using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// This controller may not be necessary.
    /// </summary>
    [OwinAuthorize]
    [RoutePrefix("api/v1/landing")]
    public class LandingPageController : ApiController
    {
        private readonly ILandingPageServices _landingPageServices;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageController"/> class.
        /// </summary>
        /// <param name="landingPageServices">
        /// An instance of the <see cref="LandingPageServices"/> class.
        /// </param>
        /// <exception cref="ArgumentNullException">landingPageServices</exception>
        public LandingPageController(
            ILandingPageServices landingPageServices)
        {
            _landingPageServices = landingPageServices ?? throw new ArgumentNullException(nameof(landingPageServices));
        }

        // Landing Page information is retrieved via the GetPageData() method
        // of the LoginController. [@ ".../login/pageData/{electionId}"]

    }
}
