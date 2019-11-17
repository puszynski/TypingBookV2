using System.Text.RegularExpressions;
using TypingBook.Extensions;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;

namespace TypingBook.Services
{
    public class BookContentService : IBookContentService
    {
        private readonly IBookRepository _bookRepository;

        public BookContentService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void ReeditBooksContent()
        {
            var books = _bookRepository.GetAllBooksAsync();

            foreach (var item in books.Result)
            {
                item.Content = TransformeBookContent(item.Content);
            }
        }

        string TransformeBookContent(string input)
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
