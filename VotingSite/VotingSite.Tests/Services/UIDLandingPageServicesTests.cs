using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using VotingSite.DAL;
using VotingSite.Domain;
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
        //public UIDLandingPageServices( ILandingPageDataAccess landingPageDataAccess )
        //async Task<LandingPgViewModel> GetLandingPageDataAsync(int electionId)
        [TestMethod]
        public async Task GivenValidArgument_GetLandingPageDataAsync_ShouldReturn_LandingPgViewModel()
        {
            // Arrange
            const int expectedElectionId = 1;

            // create fake data to return to the 
            var mockLandingPageDataAccess = new Mock<ILandingPageDataAccess>();
            // Method To Test: async Task<LandingPageViewData> GetLandingPageViewData(int electionId)
            mockLandingPageDataAccess.Setup(mut => mut.GetLandingPageViewData(expectedElectionId))
                .Returns<int>(eId =>
                {
                    LandingPageViewData fakeResult;

                    // so that this only returns the contests IF the electionId == 1.
                    if (eId == expectedElectionId)
                    {
                        fakeResult = new LandingPageViewData
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
                                    HtmlContestId = "ContestItem_1_Id",
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
                        };
                    }
                    else
                    {
                        fakeResult = new LandingPageViewData();
                    }

                    return Task.FromResult(fakeResult);
                });

            //var htmlContestId = $"ContestItem_{tempId}_Id";
            const string expectedHtmlContestIdString1 = "ContestItem_1_Id";
            const string expectedHtmlContestIdString2 = "ContestItem_2_Id";
            const string expectedContest2TitleString = "Position B";

            // class UIDLandingPageServices ctor( ILandingPageDataAccess landingPageDataAccess )
            var mockLandingPageServices = new UIDLandingPageServices(mockLandingPageDataAccess.Object);
            var expectedReturnType = typeof(LandingPgViewModel);

            // Act
            var landingPgViewModel = await mockLandingPageServices.GetLandingPageDataAsync(expectedElectionId);

            // Assert
            Assert.IsNotNull(landingPgViewModel);
            Assert.IsInstanceOfType(landingPgViewModel, expectedReturnType);
            Assert.AreEqual(2, landingPgViewModel.BallotData.Contests.Count);

            Assert.AreEqual(landingPgViewModel.BallotData.Contests[0].HtmlContestId, expectedHtmlContestIdString1);
            Assert.AreEqual(landingPgViewModel.BallotData.Contests[1].HtmlContestId, expectedHtmlContestIdString2);

            Assert.AreEqual(landingPgViewModel.BallotData.Contests[1].Title, expectedContest2TitleString);
        }
    }
}
