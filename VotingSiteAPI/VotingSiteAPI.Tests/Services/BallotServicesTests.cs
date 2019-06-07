using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Tests.Services
{
    /// <summary>
    /// Tests for the <c>BallotServices</c> APIs
    /// </summary>
    [TestClass]
    public class BallotServicesTests
    {
        [TestMethod]
        public void GivenValidInput_GetContestsByElectionId_ShouldReturnAppropriateContests()
        {
            // testing this guy
            // IEnumerable<Contest> GetContestsByElectionId(int electionId)

            // Arrange 
            // ContestsTestData.json
            var contests = new List<Contest>
            {
                new Contest
                {
                    Id = 1,
                    BallotTypeId = 1,
                    Title = "Position A",
                    Description = "blank for CalPERS",
                    MaxVotes = 200,
                    SortOrder = 1
                },
                new Contest
                {
                    Id = 2,
                    BallotTypeId = 1,
                    Title = "Position B",
                    MaxVotes = 200,
                    SortOrder = 2
                },
                new Contest
                {
                    Id = 3,
                    BallotTypeId = 2,
                    Title = "Bank CEO",
                    MaxVotes = 1,
                    SortOrder = 2
                },
                new Contest
                {
                    Id = 4,
                    BallotTypeId = 2,
                    Title = "Bank Chairman",
                    MaxVotes = 1,
                    SortOrder = 1
                }
            };

            var ballotTypeMappings = new List<BallotTypeMapping>
            {
                new BallotTypeMapping
                {
                    Id = 1,
                    BallotTypeId = 1,
                    ContestId = 1
                },
                new BallotTypeMapping
                {
                    Id = 2,
                    BallotTypeId = 2,
                    ContestId = 2
                }
            };

            var ballotTypes = new List<BallotType>
            {
                new BallotType
                {
                    Id = 1,
                    ElectionId = 1
                },
                new BallotType
                {
                    Id = 2,
                    ElectionId = 2
                }
            };

            var elections = new List<Election>
            {
                new Election
                {
                    Id = 1,
                    ElectionName = "Test ElectionName",
                    OpenDate = DateTime.Parse("2019-04-18T00:00:00"),
                    CloseDate = DateTime.Parse("2019-04-29T11:59:59")
                },
                new Election
                {
                    Id = 2,
                    ElectionName = "Test 2nd Election",
                    OpenDate = DateTime.Parse("2019-05-01T00:00:00"),
                    CloseDate = DateTime.Parse("2019-05-06T11:59:59")
                }
            };

            // mock the Contests Repository
            var mockRepo = new Mock<IContestsRepository>();
            mockRepo.Setup(mut => mut.GetContestsByElectionId(It.IsAny<int>()))
                .Returns(
                    new Func<int, IEnumerable<Contest>>(
                        id =>
                        {
                            var retrievedContests =
                                (from contest in contests
                                    join btm in ballotTypeMappings on contest.BallotTypeId equals btm.BallotTypeId
                                    join bt in ballotTypes on btm.BallotTypeId equals bt.Id
                                    join e in elections on bt.Id equals e.Id
                                    where e.Id == id
                                    select contest);

                            return retrievedContests;
                        }));

            // IEnumerable<Contest> GetContestsByElectionId(int electionId)
            var ballotServices = new ContestServices(mockRepo.Object);

            var results = ballotServices.GetContestsByElectionId(1);

            mockRepo.Verify(x => x.GetContestsByElectionId(1), Times.Once);

            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(IEnumerable<Contest>));
            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public void GivenInvalidInput_GetContestsByElectionId_ShouldReturnAnEmptyCollection()
        {
            // Arrange 
            var contests = new List<Contest>
            {
                new Contest
                {
                    Id = 1,
                    BallotTypeId = 1,
                    Title = "Position A",
                    Description = "blank for CalPERS",
                    MaxVotes = 200,
                    SortOrder = 1
                },
                new Contest
                {
                    Id = 2,
                    BallotTypeId = 1,
                    Title = "Position B",
                    MaxVotes = 200,
                    SortOrder = 2
                },
                new Contest
                {
                    Id = 3,
                    BallotTypeId = 2,
                    Title = "Bank CEO",
                    MaxVotes = 1,
                    SortOrder = 2
                },
                new Contest
                {
                    Id = 4,
                    BallotTypeId = 2,
                    Title = "Bank Chairman",
                    MaxVotes = 1,
                    SortOrder = 1
                }
            };

            var ballotTypeMappings = new List<BallotTypeMapping>
            {
                new BallotTypeMapping
                {
                    Id = 1,
                    BallotTypeId = 1,
                    ContestId = 1
                },
                new BallotTypeMapping
                {
                    Id = 2,
                    BallotTypeId = 2,
                    ContestId = 2
                }
            };

            var ballotTypes = new List<BallotType>
            {
                new BallotType
                {
                    Id = 1,
                    ElectionId = 1
                },
                new BallotType
                {
                    Id = 2,
                    ElectionId = 2
                }
            };

            var elections = new List<Election>
            {
                new Election
                {
                    Id = 1,
                    ElectionName = "Test ElectionName",
                    OpenDate = DateTime.Parse("2019-04-18T00:00:00"),
                    CloseDate = DateTime.Parse("2019-04-29T11:59:59")
                },
                new Election
                {
                    Id = 2,
                    ElectionName = "Test 2nd Election",
                    OpenDate = DateTime.Parse("2019-05-01T00:00:00"),
                    CloseDate = DateTime.Parse("2019-05-06T11:59:59")
                }
            };

            // mock the Contests Repository
            var mockRepo = new Mock<IContestsRepository>();
            mockRepo.Setup(mut => mut.GetContestsByElectionId(It.IsAny<int>()))
                .Returns(
                    new Func<int, IEnumerable<Contest>>(
                        id =>
                        {
                            var retrievedContests =
                                (from contest in contests
                                 join btm in ballotTypeMappings on contest.BallotTypeId equals btm.BallotTypeId
                                 join bt in ballotTypes on btm.BallotTypeId equals bt.Id
                                 join e in elections on bt.Id equals e.Id
                                 where e.Id == id
                                 select contest);

                            return retrievedContests;
                        }));

            var ballotServices = new ContestServices(mockRepo.Object);

            var results = ballotServices.GetContestsByElectionId(0);

            mockRepo.Verify(x => x.GetContestsByElectionId(0), Times.Once);

            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(IEnumerable<Contest>));
            Assert.AreEqual(0, results.Count());
        }

    }
}
