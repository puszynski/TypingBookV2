using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypingBook.Models;

namespace TypingBook.Repositories.IReporitories
{
    public interface IBookRepository
    {
        Book GetBookByID(int id);
        void UpdateBook(Book model);
        void CreateBook(Book model);
    }
}
