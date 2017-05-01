namespace RangeVotingBot.Models
{
    public enum ErrorCode
    {
        VoterNotAuthorized
    }
    public class Error
    {
        public Error(ErrorCode errorCode, string message=null)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public ErrorCode ErrorCode { get; private set; }
        public string Message { get; private set; }
    }
}