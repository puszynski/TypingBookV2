using System.Collections.Generic;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Data
{
    public interface ISQLiteDapperRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookByID(int id);
        Task<Book> GetByIDAsync(int id);

        void UpdateBook(Book model);
    }
}
