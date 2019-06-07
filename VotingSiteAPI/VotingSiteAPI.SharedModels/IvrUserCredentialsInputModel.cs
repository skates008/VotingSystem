

namespace VotingSiteAPI.SharedModels
{
    /// <summary>
    /// The name really says it all.
    /// </summary>
    // ReSharper disable InconsistentNaming
    public class IvrUserCredentialsInputModel
    {
        public int ElectionID { get; set; }
        public string PIN { get; set; }
        public string SSN { get; set; }
    }
}
