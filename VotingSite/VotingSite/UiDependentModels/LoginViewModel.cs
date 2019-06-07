
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace VotingSite.UiDependentModels
{
    /// <summary>
    /// This model is here because we need access to the [AllowHtml]
    /// attribute, which means access to <see cref="System.Web.Mvc"/>
    /// </summary>
    public class LoginViewModel
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

        public string LoginIdLabelTxt { get; set; } // LoginIDLabelTxt (length: 50)
        public string LoginPinLabelTxt { get; set; } // LoginPINLabelTxt (length: 50)

        [Required(ErrorMessage = "Please enter the PIN number.")]
        [MinLength(8, ErrorMessage = "Your PIN number must be at least 8 digits.")]
        [MaxLength(8, ErrorMessage = "Your PIN number should only be 8 digits.")]
        [RegularExpression("\\d+", ErrorMessage = "Your PIN number must only be numbers.")]
        public string LoginId { get; set; } // LoginID (length: 50)

        [Required(ErrorMessage = "Please enter the last 4 digits of your SSN.")]
        [MinLength(4, ErrorMessage = "The last 4 digits of your SSN should be at least 4 digits.")]
        [MaxLength(4, ErrorMessage = "The last 4 digits of your SSN should only be 4 digits.")]
        [RegularExpression("\\d+", ErrorMessage = "The last 4 digits of your SSN should only be numbers.")]
        public string LoginPin { get; set; } // LoginPIN (length: 50)

        public bool? VoteCompleted { get; set; } // VoteCompleted

        // From Election
        public string ElectionName { get; set; } // ElectionName (length: 50)

        public System.DateTime? OpenDate { get; set; } // OpenDate

        public System.DateTime? CloseDate { get; set; } // CloseDate

        [AllowHtml]
        public string LoginScreenOpenMessage { get; set; } // LoginScreenOpenMessage (length: 2048)

        public string LoginScreenCloseMessage { get; set; } // LoginScreenCloseMessage (length: 2048)

        public string LandingPageTitle { get; set; } // LandingPageTitle (length: 50)

        [AllowHtml]     // just in case they want a link in this as well.
        public string LandingPageMessage { get; set; } // LandingPageMessage (length: 50)
    }
}
