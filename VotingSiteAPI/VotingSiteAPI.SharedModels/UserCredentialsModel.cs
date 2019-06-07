
namespace VotingSiteAPI.SharedModels
{
    public class UserCredentialsModel
    {
        public string UsernameOrId { get; set; }

        public string PasswordOrPin { get; set; }

        public int VoterId { get; set; }
        public int ElectionId { get; set; }
        public int BallotTypeId { get; set; }
    }
}
