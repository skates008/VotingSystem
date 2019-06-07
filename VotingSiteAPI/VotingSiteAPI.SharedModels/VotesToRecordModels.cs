
using System.Collections.Generic;


namespace VotingSiteAPI.SharedModels
{
    /// <summary>
    /// This is the class used as the parameter to the 'recordVotes' endpoint.
    /// </summary>
    public class RecordVotesInputModel
    {
        public int VoterId { get; set; }

        public List<VoteToRecord> VotesToRecord { get; set; }
    }

    public class VoteToRecord
    {
        // ReSharper disable once InconsistentNaming
        public int CandidateID { get; set; }

        public string VoteDate { get; set; }
    }
}
