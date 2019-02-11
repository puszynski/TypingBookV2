using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Data
{
    public interface ISQLiteDapperRepository
    {
        IEnumerable<Book> GetAll();
        Book GetByID(int id);
        Task<Book> GetByIDAsync(int id);
    }
}
