using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.Tests.Services
{
    /// <summary>
    /// Summary description for CandidatesServicesTests
    /// </summary>
    [TestClass]
    public class CandidatesServicesTests
    {
        [TestMethod]
        public void VerifyThat_GivenAContestId_GetCandidatesByContestId_ReturnsOnlyMatchingRows()
        {
            // Arrange 
            var testCandidates = new List<Candidate>
            {
                new Candidate
                {
                    Id = 1,
                    ElectionId = 1,
                    ContestId = 1,
                    CandidateName = "John Doe",
                    ShortDescription = "Traffic Officer",
                    LongDescription =
                        "This is where the Candidate's looooooooooooooonnnnnnnnnnnnnong description goes.",
                    SortOrder = 1
                },
                new Candidate
                {
                    Id = 2,
                    ElectionId = 1,
                    ContestId = 2,
                    CandidateName = "Elizabeth Banks",
                    ShortDescription = "Actor",
                    LongDescription = "Great Actress.. voiced Wild Style in the Lego Movie!!",
                    SortOrder = 2
                },
                new Candidate
                {
                    Id = 3,
                    ElectionId = 1,
                    ContestId = 1,
                    CandidateName = "Jack Smith",
                    ShortDescription = "Certified Public Accountant",
                    LongDescription = "I like, do people's taxes and stuff.",
                    SortOrder = 3
                },
                new Candidate
                {
                    Id = 4,
                    ElectionId = 1,
                    ContestId = 1,
                    CandidateName = "Mary Jones",
                    ShortDescription = "Investment Officer",
                    LongDescription =
                        "I help people figure out which stocks they should buy..Stuff like that.",
                    SortOrder = 4
                },
                new Candidate
                {
                    Id = 5,
                    ElectionId = 1,
                    ContestId = 1,
                    CandidateName = "Hank Brown",
                    ShortDescription = "Budget Analyst",
                    LongDescription =
                        "I help companies figure out how to efficiently spend their monies.",
                    SortOrder = 5
                }
            };

            var mockRepo = new Mock<ICandidatesRepository>();
            mockRepo.Setup(mut => mut.GetMany(It.IsAny<Expression<Func<Candidate, bool>>>()))
                .Returns(
                    new Func<Expression<Func<Candidate, bool>>, IEnumerable<Candidate>>(expr =>
                    {
                        var rows = testCandidates.Where(expr.Compile());
                        return rows;
                    }));

            var candidateServices = new CandidatesServices(mockRepo.Object);

            // Act
            var results = candidateServices.GetCandidatesByContestId(1);

            // Assert 
            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count());
        }

        [TestMethod]
        public void VerifyThat_GetCandidatesByContestId_ReturnsRowsSortedBySortOrderAsc()
        {
            // Arrange 
            var testCandidates = new List<Candidate>
            {
                new Candidate
                {
                    Id = 10,
                    ElectionId = 2,
                    ContestId = 2,
                    CandidateName = "John Doe",
                    ShortDescription = "Traffic Officer",
                    LongDescription =
                        "This is where the Candidate's looooooooooooooonnnnnnnnnnnnnong description goes.",
                    SortOrder = 1
                },
                new Candidate
                {
                    Id = 11,
                    ElectionId = 2,
                    ContestId = 2,
                    CandidateName = "Elizabeth Banks",
                    ShortDescription = "Actor",
                    LongDescription = "Great Actress.. voiced Wild Style in the Lego Movie!!",
                    SortOrder = 4
                },
                new Candidate
                {
                    Id = 12,
                    ElectionId = 2,
                    ContestId = 2,
                    CandidateName = "Jack Smith",
                    ShortDescription = "Certified Public Accountant",
                    LongDescription = "I like, do people's taxes and stuff.",
                    SortOrder = 3
                },
                new Candidate
                {
                    Id = 13,
                    ElectionId = 2,
                    ContestId = 2,
                    CandidateName = "Mary Jones",
                    ShortDescription = "Investment Officer",
                    LongDescription =
                        "I help people figure out which stocks they should buy..Stuff like that.",
                    SortOrder = 5
                },
                new Candidate
                {
                    Id = 5,
                    ElectionId = 1,
                    ContestId = 2,
                    CandidateName = "Hank Brown",
                    ShortDescription = "Budget Analyst",
                    LongDescription =
                        "I help companies figure out how to efficiently spend their monies.",
                    SortOrder = 2
                }
            };

            var mockRepo = new Mock<ICandidatesRepository>();
            mockRepo.Setup(mut => mut.GetMany(It.IsAny<Expression<Func<Candidate, bool>>>()))
                .Returns(
                    new Func<Expression<Func<Candidate, bool>>, IEnumerable<Candidate>>(expr =>
                    {
                        var rows = testCandidates.Where(expr.Compile()).OrderBy(can => can.SortOrder);
                        return rows;
                    }));

            var candidateServices = new CandidatesServices(mockRepo.Object);

            // Act
            var results = candidateServices.GetCandidatesByContestId(2).ToList();

            // Assert 
            Assert.IsNotNull(results);
            Assert.AreEqual("Hank Brown", results[1].CandidateName);
            Assert.AreEqual("Mary Jones", results[4].CandidateName);
        }


    }
}
