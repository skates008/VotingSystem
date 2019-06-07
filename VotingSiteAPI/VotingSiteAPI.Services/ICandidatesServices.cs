
using System.Collections.Generic;

using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Domain.Models;


namespace VotingSiteAPI.Services
{
    public interface ICandidatesServices
    {
        /// <summary>
        /// Gets the candidates by contest identifier.
        /// </summary>
        /// <param name="contestId">
        /// An integer containing the contest identifier.
        /// </param>
        /// <returns>
        /// IEnumerable&lt;Candidate&gt;
        /// </returns>
        IEnumerable<Candidate> GetCandidatesByContestId(int contestId);


        CandidateIvrResultModel GetCandidatesForIvrSystem(string contestIdString);
    }
}
