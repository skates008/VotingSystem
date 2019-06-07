
// SKF 05-May-2019
// I simply do not want to move the whole of this solution into the main/UI
// project, thus this DTO will live in the lower layers and get mapped into 
// the actual LoginViewModel in the UI layer.

namespace VotingSite.Domain
{
    public class LoginViewData
    {
        /// <summary>
        /// This flag will be set by the Service/BL layer code to indicate
        /// which message to show, and whether or not to show the login
        /// fields.
        /// </summary>
        public bool VotingIsOpen { get; set; }

        // FROM / for a Voter record
        public string VoterName { get; set; } // VoterName (length: 50)

        public string UserIp { get; set; }

        public string BrowserAgent { get; set; }

        public string Affiliation { get; set; } // Affiliation (length: 50)

        public int? ElectionId { get; set; } // ElectionID

        public int? BallotType { get; set; } // BallotType

        public string LoginId { get; set; } // LoginID (length: 50)

        public string LoginPin { get; set; } // LoginPIN (length: 50)

        public bool? VoteCompleted { get; set; } // VoteCompleted

        // From Election
        public string ElectionName { get; set; } // ElectionName (length: 50)

        public System.DateTime? OpenDate { get; set; } // OpenDate

        public System.DateTime? CloseDate { get; set; } // CloseDate

        public string LoginScreenOpenMessage { get; set; } // LoginScreenOpenMessage (length: 2048)

        public string LoginScreenCloseMessage { get; set; } // LoginScreenCloseMessage (length: 2048)

        public string LoginIdLabelTxt { get; set; } // LoginIDLabelTxt (length: 50)

        public string LoginPinLabelTxt { get; set; } // LoginPINLabelTxt (length: 50)

        public string LandingPageTitle { get; set; } // LandingPageTitle (length: 50)

        public string LandingPageMessage { get; set; } // LandingPageMessage (length: 50)
    }
}
