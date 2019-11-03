using System.Collections.Generic;
using System.Threading.Tasks;
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
                //item.Content;
            }
        }
    }
}
