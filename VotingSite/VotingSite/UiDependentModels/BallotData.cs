
using System.Collections.Generic;

using VotingSite.Domain;


namespace VotingSite.UiDependentModels
{
    public class BallotData
    {
        public int TotalContests => Contests.Count;

        public List<ContestDto> Contests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BallotData"/> class.
        /// <para>
        /// And is here to make sure that the <c>Contests</c> property may be
        /// empty, but won't be null.
        /// </para>
        /// </summary>
        public BallotData()
        {
            Contests = new List<ContestDto>();
        }
    }
}
