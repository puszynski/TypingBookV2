using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TypingBook.Data;
using TypingBook.Models;
using TypingBook.Repositories;

namespace Tests.Repositories
{
    class BookRepositoryTest
    {
        public Book GetBookByID(int id)
        {
            //what if you delete book => exeption in single ??
            var applicationDbContextMock = new Mock<ApplicationDbContext>();
            var bookRepository = new BookRepository(applicationDbContextMock.Object);
            //TODO


            return _db.Books.Single(x => x.Id == id);
        }


    }
}
