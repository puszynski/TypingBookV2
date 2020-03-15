using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using TypingBook.Extensions;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;

namespace TypingBook.Services
{
    public class BookContentService : IBookContentService
    {
        private readonly IBookRepository _bookRepository;

        public BookContentService()
        {

        }

        public BookContentService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void ReeditBooksContent()
        {
            var books = _bookRepository.GetAllBooksAsync();

            foreach (var item in books.Result)
            {
                item.Content = FormateBookContent(item.Content);
            }
        }

        public string CreateBookPagesJSON(string input)
        {
            var formatedInput = FormateBookContent(input);
            var bookPages = DivideBook(formatedInput);
            var bookPagesJSON = JsonSerializer.Serialize(bookPages);
            return bookPagesJSON;
        }

        public string FormateBookContent(string input)
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


        List<string> DivideBook(string bookContent)
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
                if (bookPages.Count == 34)
                {
                    //testy
                }
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
    }
}
