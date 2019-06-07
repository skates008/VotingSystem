

namespace VotingSiteAPI.Domain.Models
{
    /// <summary>
    /// This is the results for the /api/v1/electioninfo
    /// /2135551234 | ?called_address="2135551234" API call.
    /// <para>
    /// Only the Phone System (IVR) will be using this.
    /// </para>
    /// </summary>
    public class ElectionIdOpenAndClosed
    {
        // ReSharper disable InconsistentNaming
        public string Election_ID { get; set; }
        public string Open_DateTime { get; set; }
        public string Close_DateTime { get; set; }
    }
}
