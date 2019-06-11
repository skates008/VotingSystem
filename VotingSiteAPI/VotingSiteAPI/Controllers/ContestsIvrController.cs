using System;
using System.Web.Http;

using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Domain.Models;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Controllers
{
    // ReSharper disable once StringLiteralTypo
    [RoutePrefix("api/v1/ivrcontests")]
    public class ContestsIvrController : ApiController
    {
        private readonly IWebConfigContainer _webConfigContainer;
        private readonly IContestServices _contestServices;
        private readonly ICandidatesServices _candidatesServices;

        public ContestsIvrController(
            IWebConfigContainer webConfigContainer,
            IContestServices contestServices,
            ICandidatesServices candidatesServices)
        {
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _contestServices = contestServices ?? throw new ArgumentNullException(nameof(contestServices));
            _candidatesServices = candidatesServices ?? throw new ArgumentNullException(nameof(candidatesServices));
        }

        /// <summary>
        /// Gets the contests for IVR system [by election id].
        /// </summary>
        /// <param name="electionId">
        /// The election identifier.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ContestIvrResultModel"/>
        /// </returns>
        /// <remarks>
        /// ReSharper disable once CommentTypo
        /// GET .../api/v1/ivrcontests/{electionId}
        /// </remarks>
        [OwinAuthorize]
        [HttpGet]
        [Route("")] // allows for QueryString OR MVC-style parameters on the Url
        [Route("{electionId}")]
        public IHttpActionResult GetContestsForIvrSystem(int electionId)
        {
            ContestIvrResultModel results;

            try
            {
                // how this should be set in the client
                //httpClient.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", "Your_token");

                var eId = Convert.ToString(electionId);
                results = _contestServices.GetContestsForIvrSystem(eId);
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(results);
        }


    }
}
