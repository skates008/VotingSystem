
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;

using VotingSite.DataAccessServices;
using VotingSite.Domain;
using VotingSite.UiDependentModels;
using VotingSite.UiDependentServices;


namespace VotingSite.Controllers
{
    public class LandingController : AsyncController
    {
        //private readonly int _currentElectionId;
        private readonly IWebConfigContainer _webConfigContainer;
        private readonly IUIDLandingPageServices _landingPageServices;

        public LandingPgViewModel LandingPgViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingController"/> class.
        /// </summary>
        public LandingController(
            IWebConfigContainer webConfigContainer,
            IUIDLandingPageServices landingPageServices)
        {
            _webConfigContainer = webConfigContainer ??
                                  throw new ArgumentNullException(nameof(webConfigContainer));
            _landingPageServices = landingPageServices ??
                                   throw new ArgumentNullException(nameof(landingPageServices));

            // initialize this by creating a new/empty instance
            this.LandingPgViewModel = new LandingPgViewModel();
        }

        /// <summary>
        /// While this should be a GET, making it a POST keeps the data out
        /// of the Url.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            // Must get the data to load into the LandingPageViewModel because
            // passing it to this method puts all of the data into the Url.
            //var landingPgViewModel = this.LandingPgViewModel;

            // Coming Soon!
            LandingPgViewModel landingPgViewModel =
                await _landingPageServices.GetLandingPageDataAsync(_webConfigContainer.ElectionId);

            ////// build the 'html Id' value before adding to the contests collection
            ////var tempId = 1;
            ////var htmlContestId = $"ContestItem_{tempId}_Id";
            ////landingPgViewModel.BallotData.Contests.Add(new ContestDto
            ////{
            ////    Id = 1,
            ////    HtmlContestId = htmlContestId,
            ////    Title = "Position A",
            ////    MaxVotes = 2,
            ////    VotesCast = 0,
            ////    SortOrder = 1
            ////});

            ////landingPgViewModel.BallotData.Contests.Add(new ContestDto
            ////{
            ////    Id = 2,
            ////    HtmlContestId = "ContestItem_2_Id",
            ////    Title = "Position B",
            ////    MaxVotes = 2,
            ////    VotesCast = 0,
            ////    SortOrder = 2
            ////});

            ////landingPgViewModel.ElectionId = _webConfigContainer.ElectionId;
            ////landingPgViewModel.ElectionName = "2019 Member-at-large Board Election";
            ////landingPgViewModel.LandingPageTitle = "Success!";
            ////landingPgViewModel.LandingPageMessage =
            ////    "<p>Your PIN and the last 4 digits of your Social Security number have been accepted, and you are now logged in.</p><p>By submitting my vote online, I HEARBY CERTIFY UNDER PENALTY OF PERJURY that I was an active or retired member of the California Public Employees' Retirement System (CalPERS) on or before July 1, 2019 and therefore I am eligible to participate in this election and that I personally vote the ballot to be submitted online.</p><p>Select <b>Start Voting</b> below to access your ballot and begin voting.</p><p>If you need help, select <b>Help</b> in the menu bar.</p>";

            // Passing it this way will send it to the layout:
            // Shared\_LandAndVoteLayout.cshtml
            // AND Views\Landing\Landing.cshtml
            return View("Landing", landingPgViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public ActionResult IndexPost(LandingPgViewModel landingPgViewModel)
        {
            if (!ModelState.IsValid)
            {
                // go back and try again
                //return View( )
            }


            return View("Error");
        }

        [HttpPost]
        public ActionResult SelectContest(int contestId)
        {
            // TODO: set up a test for this method (?) 

            // TODO: Will need to send the user to a 'Voting Page' as I'm calling it.

            var dContestId = contestId;

            try
            {
                Debug.WriteLine($"ContestId: {dContestId}");
            }
            catch (Exception oEx)
            {
                //loggerInst.LogCritical(oEx);

                return Json(new { updateStatus = "error", errorMsg = oEx.Message, errorStackTrace = oEx.StackTrace });
            }

            return Json(new { updateStatus = "ok" });
        }

    }
}