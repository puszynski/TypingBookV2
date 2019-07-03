using System;
using System.Collections.Generic;

namespace TypingBook.Extensions
{
    public static class EnumExtensions
    {
        public static string ToStr<T>(this T e) where T : Enum
        {
            return e.ToStr();
        }

        public static IEnumerable<int> ConvertEnumSumToIntArray(this int enumSum)
        {
            for (int i = 1; i <= enumSum; i*=2)
            {
                if ((enumSum & i) == i)
                    yield return i;
            }
        }
    }
}