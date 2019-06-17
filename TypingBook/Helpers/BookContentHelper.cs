using System.Text.RegularExpressions;
using TypingBook.Extensions;

namespace TypingBook.Helpers
{
    public class BookContentHelper
    {
        public string TransformeBookContent(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // replace special char
            var charsToReplece = new char[] { ' ', ';', ',', '\r', '\t', '\n' };
            var result = input.Replace(charsToReplece, ' ');

            // replace any kind of whitespace (e.g. tabs, newlines, {doublespaces??} etc.)
            result = Regex.Replace(result, @"\s+", " ");

            return result;
        }
    }
}
