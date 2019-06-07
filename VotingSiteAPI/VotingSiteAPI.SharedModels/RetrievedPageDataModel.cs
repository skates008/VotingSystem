
namespace VotingSiteAPI.SharedModels
{
    public class RetrievedPageDataModel
    {
        public System.DateTime? OpenDate { get; set; } // OpenDate

        public System.DateTime? CloseDate { get; set; } // CloseDate

        public string LoginScreenOpenMessage { get; set; } // LoginScreenOpenMessage (length: 2048)

        public string LoginScreenCloseMessage { get; set; } // LoginScreenCloseMessage (length: 2048)

        public string LoginIdLabelTxt { get; set; } // LoginIDLabelTxt (length: 50)

        public string LoginPinLabelTxt { get; set; } // LoginPINLabelTxt (length: 50)

        public string LandingPageTitle { get; set; } // LandingPageTitle (length: 50) -- Cache this for use on the landing page

        public string LandingPageMessage { get; set; } // LandingPageMessage (length: 50) -- Cache this for use on the landing page

        public int ElectionId { get; set; }

        public string ElectionName { get; set; } // ElectionName (length: 50)
    }
}
