using System;
using System.Web.Http;

using VotingSiteAPI.CustomAuthFilter;
using VotingSiteAPI.Domain.Models;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("api/v1/elections")]
    public class ElectionsController : ApiController
    {
        private readonly IElectionsServices _electionsServices;
        private readonly IWebConfigContainer _webConfigContainer;

        public ElectionsController(
            IWebConfigContainer webConfigContainer,
            IElectionsServices electionsServices)
        {
            _webConfigContainer = webConfigContainer ?? throw new ArgumentNullException(nameof(webConfigContainer));
            _electionsServices = electionsServices ?? throw new ArgumentNullException(nameof(electionsServices));
        }

        /// <summary>
        /// GET .../api/v1/elections/{called_address}
        /// <para>
        /// Where {called_address} is the phone number used to call the IVR system.
        /// </para>
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="ElectionIdOpenAndClosed"/> class.
        /// </returns>
        [HttpGet]
        [Route("")]     // allows for QueryString OR MVC-style parameters on the Url
        [Route("{called_address}")]
        // ReSharper disable once InconsistentNaming
        public IHttpActionResult GetQuickElectionInfo(string called_address)
        {
            ElectionIdOpenAndClosed result;

            try
            {
                // how this should be set in the client
                //httpClient.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", "Your_token");

                result = _electionsServices.GetElectionIdOpenAndClosed(called_address);
            }
            catch (Exception oEx)
            {
                return InternalServerError(oEx);
            }

            return Ok(result);
        }



    }
}
