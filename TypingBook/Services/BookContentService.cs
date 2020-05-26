using System.Linq;
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

            foreach (var item in books.Result.Where(x => !x.IsVerified))
            {
                var bph = new BookPagesHandler(item.Content);
                item.Content = bph.Execute(); ;
            }
        }

        public string CreateBookPagesJSON(string bookContent)
        {
            var bph = new BookPagesHandler(bookContent);
            return bph.Execute();
        }
    }
}
