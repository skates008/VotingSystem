

namespace VotingSiteAPI.Domain.Models
{
    /// <summary>
    /// Represents a single record from the <c>Contests</c> table.
    /// </summary>
    public class ContestResultModel
    {
        // ReSharper disable once InconsistentNaming
        public int ContestID { get; set; } // ID|Id (Primary key)

        public int? SortOrder { get; set; } // SortOrder

        public string Title { get; set; } // Title (length: 50)

        public int? MaxVotes { get; set; } // MaxVotes

        public string ContestNameRecording { get; set; }
    }
}
