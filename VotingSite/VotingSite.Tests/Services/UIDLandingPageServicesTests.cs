using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using VotingSite.DAL;
using VotingSite.DataAccessServices;
using VotingSite.DataAccessServices.HttpClientHelpers;
using VotingSite.Domain;
using VotingSite.Services;
using VotingSite.UiDependentModels;
using VotingSite.UiDependentServices;


namespace VotingSite.Tests.Services
{
    /// <summary>
    /// Summary description for UIDLandingPageServicesTests
    /// </summary>
    [TestClass]
    public class UIDLandingPageServicesTests
    {
        public UIDLandingPageServicesTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        //public UIDLandingPageServices( ILandingPageDataAccess landingPageDataAccess )
        //async Task<LandingPgViewModel> GetLandingPageDataAsync(int electionId)
        [TestMethod]
        public async Task TestMethod1()
        {
            // Arrange
            const int expectedElectionId = 1;

            //LandingPageDataAccess(
            //  IWebConfigContainer webConfigContainer,
            //  IHttpClientProvider httpClientProvider)
            var mockWebConfigContainer = new Mock<IWebConfigContainer>();
            var mockHttpClientProvider = new Mock<IHttpClientProvider>();

            var mockLandingPageDataAccess = new Mock<ILandingPageDataAccess>();

            //async Task<LandingPageViewData> GetLandingPageViewData(int electionId)
            mockLandingPageDataAccess.Setup(mut => mut.GetLandingPageViewData(expectedElectionId))
                .Returns<int>(eId =>
                    Task.FromResult(new LandingPageViewData
                    {
                        ElectionId = eId,
                        ElectionName = "THIS IS THE TEST ELECTION NAME",
                        LandingPageTitle = "Success!",
                        LandingPageMessage = "LandingPageMessage; Welcome to our Voting system!",
                        Contests = new List<ContestDto>
                        {
                            new ContestDto
                            {
                                Id = 1,
                                HtmlContestId = "ContestItem_2_Id",
                                Title = "Position A",
                                MaxVotes = 2,
                                VotesCast = 0,
                                SortOrder = 1
                            },
                            new ContestDto
                            {
                                Id = 2,
                                HtmlContestId = "ContestItem_2_Id",
                                Title = "Position B",
                                MaxVotes = 2,
                                VotesCast = 0,
                                SortOrder = 2
                            }
                        }
                    }));

            // LandingPgViewModel

            //UIDLandingPageServices( ILandingPageDataAccess landingPageDataAccess )
            var mockLandingPageServices = new UIDLandingPageServices(
                mockLandingPageDataAccess.Object);

            //var service = new UiDependentLoginServices(mockLoginScreenDataAccess.Object);
            //var expectedReturnedType = typeof(LoginViewModel);
            //
            //// Act
            //var callResult = await service.GetLoginScreenDataAsync(expectedElectionId);
            //
            //// Assert
            //mockLoginScreenDataAccess.Verify(
            //	mut => mut.GetDataForLoginScreenAsync(expectedElectionId),
            //	Times.Once);
            //Assert.IsNotNull(callResult);
            //Assert.IsInstanceOfType(callResult, expectedReturnedType);
            //
            //Assert.AreEqual(expectedElectionId, callResult.ElectionId);
            //Assert.AreEqual("Success!", callResult.LandingPageTitle);




        }
    }
}
