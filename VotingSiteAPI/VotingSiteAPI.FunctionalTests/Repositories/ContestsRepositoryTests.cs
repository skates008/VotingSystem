
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Data.Repositories;
using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Services;


namespace VotingSiteAPI.FunctionalTests.Repositories
{
    /// <summary>
    /// Summary description for ContestsRepositoryTests
    /// </summary>
    public class ContestsRepositoryTests
    {
        public ContestsRepositoryTests()
        {
            //
            // TODO: Add constructor logic here as needed
            //
        }

        #region . Test Context .
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
        #endregion
        #region . Additional test attributes .
        ////
        //// You can use the following additional attributes as you write your tests:
        ////
        //// Use ClassInitialize to run code before running the first test in the class
        //// [ClassInitialize()]
        //// public static void MyClassInitialize(TestContext testContext) { }
        ////
        //// Use ClassCleanup to run code after all tests in a class have run
        //// [ClassCleanup()]
        //// public static void MyClassCleanup() { }
        ////
        //// Use TestInitialize to run code before running each test 
        //// [TestInitialize()]
        //// public void MyTestInitialize() { }
        ////
        //// Use TestCleanup to run code after each test has run
        //// [TestCleanup()]
        //// public void MyTestCleanup() { }
        ////
        #endregion


        ////public void TestThe_GetContestsByIdMethod_ShouldReturnTwoContests()
        ////{
        ////    //IEnumerable<Contest> GetContestsByElectionId(
        ////    //    int contestId,
        ////    //    int electionId)
        ////    var dbFactory = new DatabaseFactory();
        ////    IContestsRepository contestsRepo = new ContestsRepository(dbFactory);
        ////    //ILandingPageServices lpServices = new LandingPageServices(contestsRepo);

        ////    var results = contestsRepo.GetContestsByElectionId(1);

        ////    Assert.IsNotNull(results);
        ////    Assert.IsInstanceOfType(results, typeof(IEnumerable<Contest>));
        ////    Assert.AreEqual(2, results.Count());
        ////}
    }

}
