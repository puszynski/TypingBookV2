using System;
using System.Collections.Generic;

namespace TypingBook.Helpers
{
    public class BinarySumToIntListHelper
    {
        static BinarySumToIntListHelper _b;
        private BinarySumToIntListHelper()
        {
        }

        public static BinarySumToIntListHelper GetInstance()
        {
            if (_b == null)
                _b = new BinarySumToIntListHelper();
            return _b;
        }

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
