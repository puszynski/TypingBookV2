using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return _db.Books.Single(x => x.Id == id);
        }

        public async Task<Book> GetAsyncBookByID(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _db.Books;
        }

        public Task<List<Book>> GetAllBooksAsync()
        {
            return _db.Books.ToListAsync();
        }

        public void UpdateBook(Book model)
        {
            _db.Update(model);
        }

        public void CreateBook(Book model)
        {
            _db.Add(model);
        }

        public void RemoveBook(Book book)
        {
            _db.Books.Remove(book);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
