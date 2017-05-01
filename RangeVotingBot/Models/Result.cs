using System.Collections.Generic;
using System.Linq;

namespace RangeVotingBot.Models
{
    public class Result
    {
        public bool IsSuccess { get; private set; }

        public Result(IList<Error> errors = null)
        {
            Errors = errors;
            IsSuccess = errors == null || !errors.Any();
        }

        public IEnumerable<Error> Errors { get; private set; }
    }
    
    public class Result<T> : Result
    {
        public T Value { get; private set; }

        public Result(T value, IList<Error> errors = null) : base(errors)
        {
            Value = value;
        }
    }
}