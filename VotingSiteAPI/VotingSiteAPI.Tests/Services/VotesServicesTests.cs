using System;
using System.Text;
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
    /// Summary description for VotesServicesTests
    /// </summary>
    [TestClass]
    public class VotesServicesTests
    {
        #region Additional test attributes

        // don't really use this stuff, but I leave it here just in case. -SKF

        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void TestThe_VoteEqualityComparer_ShouldPass()
        {
            // Arrange
            var vote1VoteDate = new DateTime(2019, 6, 2, 1, 11, 23);
            var vote2VoteDate = new DateTime(2019, 6, 2, 1, 12, 48);

            var vote1 = new Vote
            {
                Id = 1,
                CandidateId = 1,
                VoteDate = vote1VoteDate,
                VoterId = 32
            };

            var vote2 = new Vote
            {
                Id = 2,
                CandidateId = 3,
                VoteDate = vote2VoteDate,
                VoterId = 32
            };

            var vote3 = new Vote
            {
                Id = 3,
                CandidateId = 1,
                VoteDate = vote1VoteDate,
                VoterId = 32
            };

            // Arrange
            // Act 
            var comparer = new VoteEqualityComparer();

            // Assert
            Assert.IsTrue(comparer.Equals(vote1, vote3));
            Assert.IsFalse(comparer.Equals(vote1, vote2));

            var hash1 = comparer.GetHashCode(vote1);
            var hash2 = comparer.GetHashCode(vote2);
            var hash3 = comparer.GetHashCode(vote3);

            Assert.AreEqual(hash1, hash3);
            Assert.AreNotEqual(hash1, hash2);
        }


        [TestMethod]
        [Ignore]
        public void GiveOneAlreadyCastVote_RemoveAnyVotesAlreadyCast_ShouldReturnOnlyVotesNotAlreadyCast()
        {
            // Arrange
            var mockVotesRepo = new Mock<IVotesRepository>();
            var mockVotersRepo = new Mock<IVotersRepository>();
            var mockCandidatesRepo = new Mock<ICandidatesRepository>();
            var votesServices = new VotesServices(mockVotesRepo.Object, mockVotersRepo.Object, mockCandidatesRepo.Object);

            List<Vote> mappedVotesToCast = new List<Vote>(3);

            var vote1VoteDate = new DateTime(2019, 6, 2, 1, 11, 23);
            var vote2VoteDate = new DateTime(2019, 6, 2, 1, 12, 48);
            var vote3VoteDate = new DateTime(2019, 6, 2, 1, 13, 53);

            var vote1 = new Vote
            {
                Id = 1,
                CandidateId = 1,
                VoteDate = vote1VoteDate,
                VoterId = 32
            };

            // this one should get dropped
            var vote2 = new Vote
            {
                Id = 2,
                CandidateId = 3,
                VoteDate = vote2VoteDate,
                VoterId = 32
            };

            var vote3 = new Vote
            {
                Id = 3,
                CandidateId = 2,
                VoteDate = vote3VoteDate,
                VoterId = 32
            };

            mappedVotesToCast.Add(vote1);
            mappedVotesToCast.Add(vote2);
            mappedVotesToCast.Add(vote3);

            var acVote1 = new Vote
            {
                Id = 10,
                CandidateId = 2,
                VoteDate = new DateTime(2019, 6, 2, 1, 08, 19),
                VoterId = 32
            };

            var acVote2 = new Vote
            {
                Id = 12,
                CandidateId = 3,
                VoteDate = vote2VoteDate,
                VoterId = 32
            };

            var acVote3 = new Vote
            {
                Id = 14,
                CandidateId = 4,
                VoteDate = new DateTime(2019, 6, 2, 1, 09, 42),
                VoterId = 32
            };

            var votesAlreadyCastForThisElection = new List<Vote>(3)
            {
                acVote1, acVote2, acVote3
            };

            // Act 


            // Assert
            //Assert.IsNotNull(results);
            //Assert.AreEqual(2, results.Count());

            Assert.IsTrue(mappedVotesToCast.Contains(vote1));
            Assert.IsFalse(mappedVotesToCast.Contains(vote2));
            Assert.IsTrue(mappedVotesToCast.Contains(vote3));
        }

        // Any more tests go here.

    }


}
