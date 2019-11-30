using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using TypingBook.Data;
using TypingBook.Models;
using TypingBook.Repositories;


namespace Tests.Repositories
{
    class BookRepositoryTest
    {
        ApplicationDbContext _dbContext;

        [SetUp]
        public void SetUp() //SetUp run again before each tests
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() ) //databaseName musy be unique for each test
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            var books = new[]
            {
                new Book { Id = 1, Title = "Title1" },
                new Book { Id = 2, Title = "Title2" },
                new Book { Id = 3, Title = "Title3" }
            };

            _dbContext.Books.AddRange(books);
            _dbContext.SaveChanges();
        }

        [Test]
        public void CreateBookTest()
        {
            //Arrange            
            var bookRepository = new BookRepository(_dbContext);
            var bookToAdd = new Book { Id = 999, Title = "AddedBook" };

            //Act
            bookRepository.CreateBook(bookToAdd);//NIE POZWALA NA UPDATE
            bookRepository.SaveChanges();
            var book = bookRepository.GetBookByID(999);

            //Assert
            Assert.AreEqual("AddedBook", book.Title);
        }

        [Test]
        public void GetBookByIDTest()
        {
            //Arrange            
            var bookRepository = new BookRepository(_dbContext);

            //Act
            var book = bookRepository.GetBookByID(1);

            //Assert
            Assert.AreEqual("Title1", book.Title);
        }
        
        [Test]
        public void GetAsyncBookByIDTest()
        {
            //Arrange            
            var bookRepository = new BookRepository(_dbContext);

            //Act
            var task = bookRepository.GetAsyncBookByID(1);

            //Assert
            Assert.AreEqual("Title1", task.Result.Title);
        }

        [Test]
        public void GetAsyncBookByIDNotExistingTest()
        {
            //Arrange            
            var bookRepository = new BookRepository(_dbContext);

            //Act
            var task = bookRepository.GetAsyncBookByID(999);

            //Assert
            Assert.IsNull(task.Result);
        }

        [Test]
        public void GetBookByIDNotExistingTest()
        {
            //Arrange            
            var bookRepository = new BookRepository(_dbContext);

            //Act
            var book = bookRepository.GetBookByID(999);

            //Assert
            Assert.IsNull(book);
        }

        [Test]
        public void GetAllBooksTest()
        {
            //Arrange            
            var bookRepository = new BookRepository(_dbContext);

            //Act
            var query = bookRepository.GetAllBooks();

            //Assert
            Assert.AreEqual(3, query.Count());
        }


        [Test]
        public void GetAllBooksAsyncTest()
        {
            //Arrange
            var bookRepository = new BookRepository(_dbContext);

            //Act
            var query = bookRepository.GetAllBooksAsync();

            //Assert
            Assert.AreEqual(3, query.Result.Count());
        }

        //[Test]
        //public void RemoveBookTest()
        //{
        //    //Arrange            
        //    var bookRepository = new BookRepository(_dbContext);
        //    var bookToDelete = new Book { Id = 1, Title = "Title1" };

        //    //Act
        //    bookRepository.RemoveBook(bookToDelete);//nie pozwala
        //    bookRepository.SaveChanges();
        //    var book = bookRepository.GetBookByID(1);

        //    //Assert
        //    Assert.IsNull(book);
        //}

        //[Test]
        //public void UpdateBookTest()
        //{
        //    //Arrange            
        //    var bookRepository = new BookRepository(_dbContext);
        //    var bookToUpdate = new Book { Id = 1, Title = "ChangedTitle" };

        //    //Act
        //    bookRepository.UpdateBook(bookToUpdate);//nie pozwala
        //    bookRepository.SaveChanges();
        //    var book = bookRepository.GetBookByID(1);

        //    //Assert
        //    Assert.AreEqual("ChangedTitle", book.Title);
        //}

        public void Dispose()
        {
            //clean databases in memory after each tests
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
