using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Controllers
{
    [RoutePrefix("api/v1/contests")]
    public class ContestsController : ApiController
    {
        private readonly IWebConfigContainer _webConfigContainer;
        private readonly IContestServices _contestServices;
        private readonly ICandidatesServices _candidatesServices;

        public ContestsController(
            IWebConfigContainer webConfigContainer,
            IContestServices contestServices,
            ICandidatesServices candidatesServices)
        {
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _contestServices = contestServices ?? throw new ArgumentNullException(nameof(contestServices));
            _candidatesServices = candidatesServices ?? throw new ArgumentNullException(nameof(candidatesServices));
        }

        /// <summary>
        /// Gets the contests to be displayed on the 'My Ballot' pane on the
        /// full-sized site, or on a separate page for Mobile.
        /// </summary>
        /// <param name="electionId">
        /// An integer containing the election identifier.
        /// </param>
        /// <returns>
        /// IHttpActionResult via Ok(data), or
        /// InternalServerError(exception), or...?
        /// </returns>
        /// <remarks>
        /// api/v1/contests/{electionId}
        /// </remarks>
        [OwinAuthorize]
        [HttpGet]
        [Route("")]
        [Route("{electionId}")]
        public IHttpActionResult GetContests(int electionId)
        {
            if (electionId <= 0)
            {
                return BadRequest("An Election Id must be specified when calling this endpoint.");
            }

            IEnumerable<Contest> contests;

            try
            {
                contests = _contestServices.GetContestsByElectionId(electionId);
            }
            catch (Exception oEx)
            {
                Debug.WriteLine(oEx);
                throw;
            }

            return Ok(contests);
        }

        /// <summary>
        /// Gets the candidates by contest identifier.
        /// </summary>
        /// <param name="contestId">The contest identifier.</param>
        /// <returns>
        /// An <see cref="IHttpActionResult"/> containing an IEnumerable&lt;Candidate&gt;
        /// </returns>
        /// <remarks>
        /// <para>
        /// This one is for the UI / website
        /// </para>
        /// api/v1/contests/ui/{electionId}
        /// </remarks>
        [OwinAuthorize]
        [HttpGet]
        [Route("{contestId}/candidates")]
        public IHttpActionResult GetCandidatesByContestId(int contestId)
        {
            if (contestId <= 0)
            {
                return BadRequest("A Contest Id of 1 or higher must be specified when calling this endpoint.");
            }

            IEnumerable<Candidate> candidates;

            try
            {
                candidates = _candidatesServices.GetCandidatesByContestId(contestId);
            }
            catch (Exception oEx)
            {
                Debug.WriteLine(oEx);

                return InternalServerError(oEx);
            }

            return Ok(candidates);
        }
    }
}
