using System.Linq;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IBookRepository
    {
        Book GetBookByID(int id);
        IQueryable<Book> GetAllBooks();

        void UpdateBook(Book model);
        void CreateBook(Book model);
        void SaveChanges();
    }
}
