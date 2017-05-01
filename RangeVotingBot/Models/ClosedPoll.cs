using System.Collections.Generic;
using System.Linq;

namespace RangeVotingBot.Models
{
    public class ClosedPoll : Poll
    {
        public IEnumerable<Voter> EligibleVoters { get; } = new List<Voter>();

        public ClosedPoll(int minGrade, int maxGrade) : base(minGrade, maxGrade)
        {
        }

        protected override bool IsVoterAuthorized(Voter voter)
        {
            return EligibleVoters.Contains(voter);
        }
    }
}