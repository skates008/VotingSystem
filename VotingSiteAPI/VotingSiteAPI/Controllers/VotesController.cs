using System;
using System.Web.Http;
using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Services;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("api/v1/votes")]
    public class VotesController : ApiController
    {
        private readonly IWebConfigContainer _webConfigContainer;
        private readonly IVotesServices _votesServices;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="VotesController"/> class.
        /// </summary>
        /// <param name="webConfigContainer">The web configuration container.</param>
        /// <param name="votesServices">The votes services.</param>
        /// <exception cref="ArgumentNullException">
        /// webConfigContainer
        /// or
        /// votesServices
        /// </exception>
        public VotesController(
            IWebConfigContainer webConfigContainer,
            IVotesServices votesServices)
        {

            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _votesServices = votesServices ?? throw new ArgumentNullException(nameof(votesServices));
        }

        /// <summary>
        /// Records the votes.
        /// </summary>
        /// <param name="votesToRecord">The votes to record.</param>
        /// <returns>
        /// An integer value indicating the status of our attempt to save
        /// their votes to the database. And, whether or not they've already
        /// voted.
        /// </returns>
        [Route("record")]
        [HttpPost]
        public IHttpActionResult RecordVotes(
            [FromBody] RecordVotesInputModel votesToRecord)
        {
            IvrVotingStatusModel result;

            try
            {
                result = _votesServices.OrchestrateVoteRecording(votesToRecord);
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(result);
        }
    }
}
