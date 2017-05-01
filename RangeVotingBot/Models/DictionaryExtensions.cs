using System.Collections.Generic;

namespace RangeVotingBot.Models
{
    public static class DictionaryExtensions
    {
        public enum AddOrReplaceResultEnum
        {
            None,
            Added,
            Replaced
        }

        public static AddOrReplaceResultEnum AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dic,
            TKey key, TValue newValue)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = newValue;
                return AddOrReplaceResultEnum.Replaced;
            }
            dic.Add(key, newValue);
            return AddOrReplaceResultEnum.Added;
        }
    }
}