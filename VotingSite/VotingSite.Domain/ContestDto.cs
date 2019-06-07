
namespace VotingSite.Domain
{
    public class ContestDto
    {
        public int Id { get; set; }

        public string HtmlContestId { get; set; }

        public string Title { get; set; }

        public int MaxVotes { get; set; }

        public int VotesCast { get; set; }

        public int SortOrder { get; set; }
    }
}
