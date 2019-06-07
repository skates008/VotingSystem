
using System.Collections.Generic;


namespace VotingSiteAPI.Domain.Models
{
    /// <summary>
    /// Encapsulates the data required by the IVR system.
    /// </summary>
    public class ContestIvrResultModel
    {
        /// <summary>
        /// Gets or sets the Contests returned from the IVR system.
        /// </summary>
        /// <remarks>
        /// The customer wants the collection to have a name, thus this
        /// 'result class' wrapper.
        /// </remarks>
        public List<ContestResultModel> Contests { get; set; }
    }
}
