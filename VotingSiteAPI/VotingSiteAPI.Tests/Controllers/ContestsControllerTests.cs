using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using VotingSiteAPI.Controllers;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Tests.Controllers
{
    [TestClass]
    public class ContestsControllerTests
    {
        private const string AuthScheme = "Bearer";
        private const string ApiKey = "ACCESS_TOKEN";

        /// <summary>
        /// Testing <c>IHttpActionResult GetContests(int electionId)</c>
        /// where passing a 0 should return a BadRequest.
        /// <para>
        /// api/v1/contests/ui/0
        /// </para>
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GivenAnIncorrectParameter_GetContests_ShouldReturn_BadRequest()
        {
            // Arrange
            const int incorrectElectionId = 0;

            //var authValues = Request.Headers.Authorization;

            //if (authValues?.Scheme == null || authValues.Parameter == null)
            //{
            //    return BadRequest();
            //}

            //if (!authValues.Scheme.Equals(_webConfigContainer.AuthScheme) ||
            //    !authValues.Parameter.Equals(_webConfigContainer.ApiKey))

            var mockWebConfigContainer = new Mock<IWebConfigContainer>();
            mockWebConfigContainer.Setup(wcc => wcc.AuthScheme).Returns(AuthScheme);
            mockWebConfigContainer.Setup(wcc => wcc.ApiKey).Returns(ApiKey);

            var mockContestServices = new Mock<IContestServices>();
            mockContestServices.Setup(mut => mut.GetContestsByElectionId(0))
                .Returns(new List<Contest>());

            // api/v1/contests/ui/{electionId}
            var mockCandidateServices = new Mock<ICandidatesServices>();
            mockCandidateServices.Setup(mut => mut.GetCandidatesByContestId(0))
                .Returns(new List<Candidate>());

            var contestsController = new ContestsController(
                mockWebConfigContainer.Object,
                mockContestServices.Object,
                mockCandidateServices.Object)
            {
                Configuration = new HttpConfiguration(),
                //var authValues = Request.Headers.Authorization;
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63190/api/v1/contests/ui/0"),
                    Headers = {Authorization = new AuthenticationHeaderValue(AuthScheme, ApiKey) }
                }
            };

            // Act
            //var response = contestsController.GetContests(incorrectElectionId);
            //var contentResult = response as OkNegotiatedContentResult<IEnumerable<Contest>>;

            // Assert
            //Assert.IsNotNull(contentResult);
            //Assert.IsNotNull(contentResult.Content);
            //Assert.IsInstanceOfType(response, typeof(IHttpActionResult));
            //Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<Contest>>));
        }

        /// <summary>
        /// Testing <c>IHttpActionResult GetContests(int electionId)</c>
        /// where passing a 0 should return a BadRequest.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GivenInvalidParameter_GetContests_ShouldReturnBadRequest()
        {
            // Arrange
            const int invalidElectionId = 0;

            var mockWebConfigContainer = new Mock<IWebConfigContainer>();

            var mockContestServices = new Mock<IContestServices>();
            mockContestServices.Setup(mut => mut.GetContestsByElectionId(0))
                .Returns(new List<Contest>());

            var mockCandidateServices = new Mock<ICandidatesServices>();
            mockCandidateServices.Setup(mut => mut.GetCandidatesByContestId(0))
                .Returns(new List<Candidate>());

            var contestsController = new ContestsController(
                mockWebConfigContainer.Object,
                mockContestServices.Object,
                mockCandidateServices.Object)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost:63190/api/v1/contests/1/candidates")
                }
            };

            // Act
            //var actionResult = contestsController.GetContests(invalidElectionId);

            //var ct = new CancellationToken();
            //var response = actionResult.ExecuteAsync(ct).Result;
            
            //// Assert
            //Assert.IsInstanceOfType(actionResult, typeof(IHttpActionResult));
            //Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            //Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // Need to test
        // [HttpGet]
        // [Route("contests/{contestId}/candidates")]
        // public IHttpActionResult GetCandidatesByContestId(int contestId)

        //[TestMethod]
        //public void GivenAValidArgument_GetCandidatesByContestId_ShouldReturnAValidCollection()
        //{
        //    // Arrange
        //    const int validContestId = 1;

        //    var mockWebConfigContainer = new Mock<IWebConfigContainer>();

        //    var mockCandidateServices = new Mock<ICandidatesServices>();
        //    mockCandidateServices.Setup(mut => mut.GetCandidatesByContestId(0))
        //        .Returns(new List<Candidate>());

        //    var mockContestServices = new Mock<IContestServices>();

        //    var contestsController = new ContestsController(
        //        mockWebConfigContainer.Object,
        //        mockContestServices.Object,
        //        mockCandidateServices.Object)
        //    {
        //        Configuration = new HttpConfiguration(),
        //        Request = new HttpRequestMessage
        //        {
        //            Method = HttpMethod.Post,
        //            RequestUri = new Uri("http://localhost:63190/api/v1/contests/1/candidates")
        //        }
        //    };

        //    // Act
        //    var response = contestsController.GetCandidatesByContestId(validContestId);
        //    var contentResult = response as OkNegotiatedContentResult<IEnumerable<Candidate>>;

        //    // Assert
        //    Assert.IsNotNull(contentResult);
        //    Assert.IsNotNull(contentResult.Content);
        //    Assert.IsInstanceOfType(response, typeof(IHttpActionResult));
        //    Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<Candidate>>));
        //}

        //[TestMethod]
        //public void GivenAValidArgument_GetCandidatesByContestId_ShouldReturnABadRequestResponse()
        //{
        //    // Arrange
        //    const int validContestId = 0;

        //    var mockWebConfigContainer = new Mock<IWebConfigContainer>();

        //    var mockCandidateServices = new Mock<ICandidatesServices>();
        //    mockCandidateServices.Setup(mut => mut.GetCandidatesByContestId(0))
        //        .Returns(new List<Candidate>());

        //    var mockContestServices = new Mock<IContestServices>();

        //    var contestsController = new ContestsController(
        //        mockWebConfigContainer.Object,
        //        mockContestServices.Object,
        //        mockCandidateServices.Object)
        //    {
        //        Configuration = new HttpConfiguration(),
        //        Request = new HttpRequestMessage
        //        {
        //            Method = HttpMethod.Post,
        //            RequestUri = new Uri("http://localhost:63190/api/v1/contests/1/candidates")
        //        }
        //    };

        //    // Act
        //    var response = contestsController.GetCandidatesByContestId(validContestId);

        //    var ct = new CancellationToken();
        //    var actionResult = response.ExecuteAsync(ct).Result;

        //    // Assert
        //    Assert.IsNotNull(actionResult);
        //    Assert.IsNotNull(actionResult.Content);
        //    Assert.IsInstanceOfType(actionResult, typeof(HttpResponseMessage));
        //    Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
        //}
    }
}
