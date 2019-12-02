using System.Collections.Generic;
using System.Text.RegularExpressions;
using TypingBook.Extensions;

namespace TypingBook.Helpers
{
    public class TypingHelper
    {
        #region Singleton
        private static TypingHelper _typingHelper;

        private TypingHelper()
        {
        }

        public static TypingHelper GetInstance()
        {
            if (_typingHelper == null)
                _typingHelper = new TypingHelper();
            return _typingHelper;
        }
        #endregion

        #region DivideBook
        /// <summary>
        /// Book.Content przechowuje prawidłowo sforatowany string: zdania rozdzielone za pomocą ". " + brak podwójnych spacji. 
        /// </summary>
        public List<string> DivideBook(string bookContent)
        {
            const int minAndMaxLenghtOfBothParts = 100;//first it was 200
            var rest = bookContent;
            var firstPart = "";
            var secondPart = "";
            var bookPages = new List<string>();
            var IsEnd = false;

            while (rest != "")
            {
                if (IsLastPart(rest))
                    firstPart = rest.Substring(0, minAndMaxLenghtOfBothParts);

                if (!IsEnd)
                    rest = rest.Replace(firstPart, "");

                if (IsLastPart(rest, true))
                {
                    secondPart = rest.Substring(0, rest.IndexOf(". ") + 1);

                    // todo
                    if (secondPart.Length >= minAndMaxLenghtOfBothParts)
                    {
                        secondPart = rest.Substring(0, rest.IndexOf(" ") + 1); // narazie - awaryjnie gdy brak ". " rozdziela po spacji
                    }
                }

                if (!IsEnd)
                {
                    if (secondPart == "")
                        rest = "";
                    else
                        rest = rest.Replace(secondPart, "").RemoveSpacesFromBeginning();
                }

                bookPages.Add(firstPart + secondPart);
                firstPart = "";
                secondPart = "";
            }
            return bookPages;



            bool IsLastPart(string input, bool IsSecondPart = false)
            {
                var result = input.Length > minAndMaxLenghtOfBothParts ? true : false;

                if (result == false && IsEnd == false)
                {
                    IsEnd = true;

                    if (IsSecondPart)
                        secondPart = rest;
                    else
                        firstPart = rest;

                    rest = "";
                }
                return result;
            }
        }
        #endregion DivideBook        
    }
}
