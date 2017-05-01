namespace RangeVotingBot.Models
{
    public class OpenPoll : Poll
    {
        public OpenPoll(int minGrade, int maxGrade) : base(minGrade, maxGrade)
        {
        }

        protected override bool IsVoterAuthorized(Voter voter)
        {
            return true;
        }
    }
}