using System;
using System.Linq;
using VotingSiteAPI.Data.Infrastructure;
using VotingSiteAPI.Data.Repositories;


namespace VotingSiteAPI.TestHarnessConApp01
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a Test Harness for the VotingSiteAPI Repository layer classes that");
            Console.WriteLine("cannot be easily tested any other way.");
            Console.WriteLine();

            var dbFactory = new DatabaseFactory();
            IContestsRepository contestsRepo = new ContestsRepository(dbFactory);
            //ILandingPageServices lpServices = new LandingPageServices(contestsRepo);

            var results = contestsRepo.GetContestsByElectionId(1);

            //Assert.IsNotNull(results);
            if (results == null)
            {
                Console.WriteLine("Method call: contestsRepo.GetContestsByElectionId(1); returned null!  EXITING.");
                return;
            }

            var numContests = results.Count();
            Console.WriteLine(numContests == 0
                ? "Method call: contestsRepo.GetContestsByElectionId(1); apparently succeeded, but returned 0 records."
                : $"Method call: contestsRepo.GetContestsByElectionId(1); -> {numContests}");

            // NOTE: as I add tests, they will of course need to be moved off into other classes, etc. -SKF


            Console.WriteLine();
            Console.Write("Hit [Enter] to continue:");
            Console.ReadLine();
            Console.WriteLine();
        }
    }
}
