using System;
using System.Collections.Generic;

namespace TypingBook.Helpers
{
    public class EnumBinarySumConverterHelper
    {
        static EnumBinarySumConverterHelper _b;
        private EnumBinarySumConverterHelper()
        {
        }

        public static EnumBinarySumConverterHelper GetInstance()
        {
            if (_b == null)
                _b = new EnumBinarySumConverterHelper();
            return _b;
        }

        // Zastąpione przez extensiona do int`a => EnumExtensions
        //public IEnumerable<int> ParseBinarySumToIntList(int enumBinarySum)
        //{
        //    var powerOfTwo = 0;

        //    for (int i = 0; powerOfTwo < enumBinarySum; i++)
        //    {
        //        powerOfTwo = (int)Math.Pow(2, i);
        //        if ((enumBinarySum & powerOfTwo) == powerOfTwo)
        //            yield return powerOfTwo;
        //    }
        //}

        // ToDelete?
        //public int? ParseSelectedListItemsToBinarySum(IEnumerable<SelectListItem> input)
        //{
        //    if(!input.Any())
        //        return null;

        //    return input.Where(x => x.Selected == true).Select(x => x.Value).Cast<int>().Sum();
        //}
    }
}
