using System.Text.RegularExpressions;

namespace TypingBook.Helpers
{
    public class BookContentHelper
    {
        public string TransformedBookContent(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var result = Regex.Replace(input, @"\s+", " "); // it will catch runs of any kind of whitespace (e.g. tabs, newlines, etc.) and replace them with a single space.
            // + remove double spaces?? zrób test czy ten regex usuwa podwójne spacje, jak nie to trzeba jeszcze dodatkowo dodać
            // + usuwanie wszelkich nietypowyych znaków

            return result;
        }
    }
}
