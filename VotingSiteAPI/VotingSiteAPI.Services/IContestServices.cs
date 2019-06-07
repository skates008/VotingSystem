
using System.Collections.Generic;

using VotingSiteAPI.Domain.Entities;
using VotingSiteAPI.Domain.Models;


namespace VotingSiteAPI.Services
{
    public interface IContestServices
    {
        /// <summary>
        /// Get's the <see cref="Contest"/>'s for the specified Election.
        /// </summary>
        /// <param name="electionId">
        /// An integer containing the <c>ElectionId</c> for which its contests
        /// must be retrieved.
        /// </param>
        /// <returns>
        /// IEnumerable&lt;Contest&gt;
        /// </returns>
        IEnumerable<Contest> GetContestsByElectionId(int electionId);

        /// <summary>
        /// Gets the <c>Contests</c> for the IVR system.
        /// </summary>
        /// <param name="electionId">
        /// A string containing the election identifier.
        /// </param>
        /// <returns>
        /// A hydrated instance of the <see cref="ContestIvrResultModel"/>
        /// class.
        /// </returns>
        ContestIvrResultModel GetContestsForIvrSystem(string electionId);
    }
}
