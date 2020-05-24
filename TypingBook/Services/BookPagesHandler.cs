using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using TypingBook.Extensions;

namespace TypingBook.Services
{
    public class BookPagesHandler
    {
        private string _bookString;
        private List<string> _bookPages;
        private string _bookPagesJSON;

        public BookPagesHandler(string bookContent)
        {
            _bookString = bookContent;
            _bookPages = new List<string>();
        }

        public string Execute()
        {
            if (string.IsNullOrWhiteSpace(_bookString))
                throw new Exception("Error: no book content in book pages handler");

            ReplaceSelectedCharToEmpty();
            RemoveWhitespacesTabsNewLinesDoublespaces();
            DivideBookV2();
            //DivideBook();
            RemoveSpaceFromStartEndForEachPage();
            CreateBookPagesJSON();

            return _bookPagesJSON;
        }

        void ReplaceSelectedCharToEmpty()
        {  
            var charsToReplece = new string[] 
            { 
                ";", 
                ","
            };

            foreach (var charToReplace in charsToReplece)
                _bookString = _bookString.Replace(charToReplace, "");
        }

        //TODO - DOUBLE SPACES SWITCH TO SINGLE SPACE, TABS AND OTHERS TO SINGLE SPACES - CHECK IT AND FIX!
        void RemoveWhitespacesTabsNewLinesDoublespaces() => _bookString = Regex.Replace(_bookString, @"\s+", " ");

        void DivideBookV2()
        {
            var bookContent = _bookString;

            while (bookContent.Length > 0)
            {
                var lengthToDevide = FindPlaceToDevide(bookContent);

                _bookPages.Add(bookContent.Substring(0, lengthToDevide));
                bookContent = bookContent.Substring(lengthToDevide, bookContent.Length - lengthToDevide);
            }
        }

        int FindPlaceToDevide(string input)
        {
            const int PAGE_TOO_LARGE = 300;

            if (input.Length <= PAGE_TOO_LARGE)
                return input.Length;

            //take 3rd sentence
            var result = input.IndexOf('.', input.IndexOf('.', input.IndexOf('.') + 1) + 1) + 1;

            if (result > PAGE_TOO_LARGE)
                //2nd
                result = input.IndexOf('.', input.IndexOf('.') + 1) + 1;

            if (result > PAGE_TOO_LARGE)
                //1st
                result = input.IndexOf('.') + 1;

            if (result > PAGE_TOO_LARGE * 2)
                throw new Exception($"Error: one of sentence is too large(More than {PAGE_TOO_LARGE * 2})");

            return result;
        }

        [Obsolete]
        void DivideBook()
        {
            var bookContent = _bookString;
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

            _bookPages = bookPages;


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

        void RemoveSpaceFromStartEndForEachPage()
        {
            var result = new List<string>();

            foreach (var bookPage in _bookPages)
                result.Add(bookPage.Trim());

            _bookPages = result;
        }

        void CreateBookPagesJSON()
        {  
            //additional encoder allow to use special characters e.g.: ą ź ć..
            _bookPagesJSON = JsonSerializer.Serialize(_bookPages, typeof(List<string>), new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All) });
        }
    }
}
