using System;
using System.Web.Http;

using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Domain.Models;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("api/v1/candidates")]
    public class CandidatesIvrController : ApiController
    {
        private readonly IWebConfigContainer _webConfigContainer;
        private readonly ICandidatesServices _candidatesServices;


        public CandidatesIvrController(
            IWebConfigContainer webConfigContainer,
            ICandidatesServices candidatesServices)
        {
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _candidatesServices = candidatesServices ?? throw new ArgumentNullException(nameof(candidatesServices));
        }

        /// <summary>
        /// Gets the candidates for IVR system.
        /// </summary>
        /// <param name="contestId">The contest identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")] // allows for QueryString OR MVC-style parameters on the Url
        [Route("{contestId}")]
        public IHttpActionResult GetCandidatesForIvrSystem(string contestId)
        {
            CandidateIvrResultModel results;

            try
            {
                results = _candidatesServices.GetCandidatesForIvrSystem(contestId);
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(results);
        }

    }
}
