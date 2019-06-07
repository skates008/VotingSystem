
using System.Collections.Generic;


namespace VotingSiteAPI.Domain.Models
{
    /// <summary>
    /// This is done like this so that the returned json has a root-named
    /// collection called, in this case, "Candidates".
    /// </summary>
    public class CandidateIvrResultModel
    {
        public IEnumerable<CandidateIvrReturnModel> Candidates { get; set; }
    }
}
