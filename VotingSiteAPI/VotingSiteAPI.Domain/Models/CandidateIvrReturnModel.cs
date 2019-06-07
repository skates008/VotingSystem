

namespace VotingSiteAPI.Domain.Models
{
    public class CandidateIvrReturnModel
    {
        // ReSharper disable once InconsistentNaming
        public string CandidateID { get; set; } // CandidateName (length: 50)

        public string CandidateName { get; set; } // CandidateName (length: 50)

        public string CandidateNameRecording { get; set; } // CandidateNameRecording (length: 255)

        public string CandidateBioRecording { get; set; } // CandidateBioRecording (length: 255)
    }
}
