using System;

namespace TypingBook.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSpacesFromBeginning(this string input)
        {
            var result = input;

            if (input == "")
                return "";

            if (input == " ")
                return "";

            while (result[0].ToString() == " ")
                result = result.Substring(1, result.Length - 1);

            return result;
        }

        public static string ShowOnly500Char(this string input)
        {
            if (input.Length < 500)
                return input;
            return input.Substring(0, 497) + "..."; 
        }

        public static string Replace(this string input, char[] charsToReplace, char newCharForReplaced)
        {
            string[] temp;

            temp = input.Split(charsToReplace, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(newCharForReplaced, temp);
        }
    }
}
