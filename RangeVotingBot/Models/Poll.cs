using System;
using System.Collections.Generic;
using System.Linq;

namespace RangeVotingBot.Models
{
    public abstract class Poll
    {
        protected Poll(int minGrade, int maxGrade)
        {
            Grade.MinValue = minGrade;
            Grade.MaxValue = maxGrade;
        }

        public Question Question { get; set; }
        public IEnumerable<Option> Options { get; } = new List<Option>();
        private AnswerDictionary Answers { get; } = new AnswerDictionary();

        public Result<AddOrReplaceAnswerResult> AddOrReplaceAnswer(AnswerKey answerKey, Grade value)
        {
            var isAuthorised = IsVoterAuthorized(answerKey.Voter);
            if (!isAuthorised)
            {
                return new Result<AddOrReplaceAnswerResult>(
                    new AddOrReplaceAnswerResult(false, DictionaryExtensions.AddOrReplaceResultEnum.None),
                    new[] {new Error(ErrorCode.VoterNotAuthorized)});
            }
            var addOrReplaceResult = Answers.AddOrReplace(answerKey, value);
            return new Result<AddOrReplaceAnswerResult>(new AddOrReplaceAnswerResult(true, addOrReplaceResult));
        }

        public OptionScore GetScore(Option option)
        {
            var keyValuePairs = Answers
                .Where(e => e.Key.Option == option)
                .ToArray();
            return new OptionScore(
                keyValuePairs.Average(e => e.Value.Value),
                keyValuePairs.Length);
        }

        public IEnumerable<KeyValuePair<Option, OptionScore>> GetResults()
        {
            return Answers.Keys
                .Select(e=>e.Option)
                .Distinct()
                .ToDictionary(option => option, GetScore)
                .OrderByDescending(e => e.Value.Score);
        }

        public class AddOrReplaceAnswerResult
        {
            public AddOrReplaceAnswerResult(bool isAuthorized, DictionaryExtensions.AddOrReplaceResultEnum isAddedOrReplaced)
            {
                IsAuthorized = isAuthorized;
                IsAddedOrReplaced = isAddedOrReplaced;
            }

            public bool IsAuthorized { get; private set; }
            public DictionaryExtensions.AddOrReplaceResultEnum IsAddedOrReplaced { get; private set; }
        }
        
        protected abstract bool IsVoterAuthorized(Voter voter);

        public class Grade
        {
            public Grade(int value)
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"should be >= {MinValue} and <= {MaxValue}");
                }
                Value = value;
            }

            public static int MinValue;
            public static int MaxValue;

            public int Value { get; private set; }
        }

        public class AnswerKey
        {
            public AnswerKey(Voter voter, Option option)
            {
                Voter = voter;
                Option = option;
            }

            public Voter Voter { get; private set; }
            public Option Option { get; private set; }
        }

        public class AnswerDictionary : Dictionary<AnswerKey, Grade>
        {
        }
    }

    public class OptionScore
    {
        public OptionScore(double score, int votes)
        {
            Score = score;
            Votes = votes;
        }

        public double Score { get; private set; }
        public int Votes { get; private set; }
    }


    public class Question
    {
        public string Value { get; set; }
    }

    public class Option
    {
        public string Value { get; set; }
    }

    public class Voter
    {
        public string Name { get; set; }
    }
}
