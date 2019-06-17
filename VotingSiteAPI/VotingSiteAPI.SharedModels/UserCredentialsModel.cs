
namespace VotingSiteAPI.SharedModels
{
    public class UserCredentialsModel
    {
        public int ElectionId { get; set; }

        public string UsernameOrId { get; set; }

        public string PasswordOrPin { get; set; }

        public string UserAgent { get; set; }

        public string UserIpAddress { get; set; }

        public int VoterId { get; set; }
        public int BallotTypeId { get; set; }
    }
}
