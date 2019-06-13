
using System.Web.Mvc;


namespace VotingSite.UiDependentModels
{
    public class LandingPgViewModel
    {
        public string LandingPageTitle { get; set; } // LandingPageTitle (length: 50)

        [AllowHtml]
        public string LandingPageMessage { get; set; } // LandingPageMessage (length: 50)

        public int ElectionId { get; set; }     // may not need this, not 100% sure yet. -SKF

        public string ElectionName { get; set; } // ElectionName (length: 50)

        // 'Ballot' specific data. Currently just contains:
        // int TotalContests => Contests.Count;
        // List<ContestDto> Contests { get; set; }
        public BallotData BallotData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BallotData"/> class.
        /// <para>
        /// Simply here to make sure that while it might be empty, it's not
        /// null.
        /// </para>
        /// </summary>
        public LandingPgViewModel()
        {
            this.BallotData = new BallotData();
        }
    }
}
