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
        public List<string> DivideBook(string bookContent)
        {
            const int minAndMaxLenghtOfBothParts = 100;
            var notDividedContent = bookContent;

            var bookPagePartOne = string.Empty;
            var bookPagePartTwo = string.Empty;

            var bookPages = new List<string>();
            var IsEnd = false;

            while (!string.IsNullOrEmpty(notDividedContent))
            {
                if (IsLastBookPage(notDividedContent))
                    bookPagePartOne = notDividedContent.Substring(0, minAndMaxLenghtOfBothParts);

                if (!IsEnd)
                    notDividedContent = notDividedContent.Replace(bookPagePartOne, "");

                if (IsLastBookPage(notDividedContent, true))
                {
                    bookPagePartTwo = notDividedContent.Substring(0, notDividedContent.IndexOf(". ") + 1);

                    // todo
                    if (bookPagePartTwo.Length >= minAndMaxLenghtOfBothParts)
                    {                        
                        bookPagePartTwo = notDividedContent.Substring(0, notDividedContent.IndexOf(" ") + 1); // narazie - awaryjnie gdy brak ". " rozdziela po spacji
                    }
                }

                if (!IsEnd)
                {
                    if (bookPagePartTwo == string.Empty)
                        notDividedContent = string.Empty;
                    else
                    {
                        if (bookPagePartTwo == " ")
                            notDividedContent = notDividedContent.RemoveSpacesFromBeginning();
                        else
                            notDividedContent = notDividedContent.Replace(bookPagePartTwo, "").RemoveSpacesFromBeginning();
                    }
                }

                bookPages.Add(bookPagePartOne + bookPagePartTwo);
                bookPagePartOne = string.Empty;
                bookPagePartTwo = string.Empty;
            }

            return bookPages;


            bool IsLastBookPage(string input, bool IsBookPagePartTwo = false)
            {
                var result = input.Length > minAndMaxLenghtOfBothParts ? true : false;

                if (result == false && IsEnd == false)
                {
                    IsEnd = true;

                    if (IsBookPagePartTwo)
                        bookPagePartTwo = notDividedContent;
                    else
                        bookPagePartOne = notDividedContent;

                    notDividedContent = "";
                }
                return result;
            }
        }
        #endregion DivideBook        
    }
}
