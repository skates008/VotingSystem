using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VotingSite.Domain
{
    public class LandingPageViewData
    {
        public int ElectionId { get; set; }     // may not need this, not 100% sure yet. -SKF

        public string ElectionName { get; set; } // ElectionName (length: 50)

        public string LandingPageTitle { get; set; } // LandingPageTitle (length: 50)

        //[AllowHtml]
        public string LandingPageMessage { get; set; } // LandingPageMessage (length: 50)

        // 'Ballot' specific data. Currently just contains:
        // int TotalContests => Contests.Count;
        // List<ContestDto> Contests { get; set; }
        //public BallotData BallotData { get; set; }

        public List<ContestDto> Contests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <c>Contests</c> collection.
        /// <para>
        /// Simply here to make sure that while it might be empty, it's not
        /// null.
        /// </para>
        /// </summary>
        public LandingPageViewData()
        {
            this.Contests = new List<ContestDto>(5);
        }
    }
}
