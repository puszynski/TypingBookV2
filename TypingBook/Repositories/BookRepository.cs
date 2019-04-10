using System.Linq;
using TypingBook.Data;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;

namespace TypingBook.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }        


        public Book GetBookByID(int id)
        {
            return _db.Books.Single(x => x.ID == id);
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _db.Books;
        }

        public void UpdateBook(Book model)
        {
            _db.Update(model);
        }

        public void CreateBook(Book model)
        {
            _db.Add(model);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
