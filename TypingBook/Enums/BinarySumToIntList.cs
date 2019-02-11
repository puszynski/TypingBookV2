using System;
using System.Collections.Generic;

namespace TypingBook.Enums
{
    public class BinarySumToIntList
    {
        public IEnumerable<int> Parse(int enumBinarySum)
        {
            var powerOfTwo = 0;

            for (int i = 0; powerOfTwo < enumBinarySum; i++)
            {
                powerOfTwo = (int)Math.Pow(2, i);
                if ((enumBinarySum & powerOfTwo) == powerOfTwo)
                    yield return powerOfTwo;
            }
        }
    }
}
