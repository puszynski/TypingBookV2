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
    }
}
