
using System.Collections.Generic;

using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.SharedModels;


namespace VotingSiteAPI.Services
{
    public interface IVotesServices
    {
        IvrVotingStatusModel OrchestrateVoteRecording(RecordVotesInputModel recordVotesInput);

        /*virtual*/
        IEnumerable<Vote> MapCastVotesToVotes(
            RecordVotesInputModel recordVotesInput);

        /*virtual*/
        bool RecordIvrVotes(
            Voter voter,
            List<Vote> votesToRecord);
    }

    public interface IVoteEqualityComparer : IEqualityComparer<Vote>
    { }
}
