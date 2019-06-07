

using System;

namespace VotingSiteAPI.SharedModels
{
    public class IvrVotingStatusModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [voting successful].
        /// </summary>
        /// <value>
        ///   <c>0</c> if [voting successful]; otherwise, <c>&lt; 0</c>.
        /// </value>
        public int VotingSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the Exception, if there was one involved and
        /// VotingSuccessful LT 0.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
