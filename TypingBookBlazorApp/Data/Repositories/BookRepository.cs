using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypingBookBlazorApp.Data.Interfaces;

namespace TypingBookBlazorApp.Data.Repositories
{
    public class BookRepository : IRepositoryAsync<Book>
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> GetByIdAsync(int id)
        {            
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book> FirstOrDefaultAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(predicate);
        }

        public async Task<Book> GetPublicByIDAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.IsVerified && x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAvailableForUser(string userId, bool isLoggerdUserAdministrator)
        {
            if (string.IsNullOrEmpty(userId))
                return await _dbContext.Books.Where(x => !x.IsPrivate && x.IsVerified).ToListAsync();

            else if (isLoggerdUserAdministrator)
                return await GetAllAsync();
            else
                return await _dbContext.Books.Where(x => x.IsVerified && !x.IsPrivate || x.UserId == userId).ToListAsync();
        }  

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetWhereAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _dbContext.Books.Where(predicate).ToListAsync();
        }
         
        public async Task<int> CountAllAsync()
        {
            return await _dbContext.Books.CountAsync();
        }

        public async Task<int> CountWhereAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _dbContext.Books.CountAsync(predicate);
        }
         
        public async Task AddAsync(Book entity)
        {
            await _dbContext.Books.AddAsync(entity);
        }

        public async Task RemoveAsync(Book entity)
        {
            _dbContext.Books.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
