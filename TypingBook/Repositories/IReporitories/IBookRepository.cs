using System.Linq;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IBookRepository
    {
        Book GetBookByID(int id);
        Task<Book> GetAsyncBookByID(int id);
        IQueryable<Book> GetAllBooks();

        void UpdateBook(Book model);
        void CreateBook(Book model);
        void SaveChanges();

        void RemoveBook(Book book);
        Task SaveAsync();
    }
}
