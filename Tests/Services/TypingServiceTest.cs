using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services;

namespace Tests.Services
{
    class TypingServiceTest
    {
        IBookRepository _bookRepository;
        IUserDataRepository _userDataRepository;

        [SetUp]
        public void SetUp()
        {
            _bookRepository = new Mock<IBookRepository>().Object;

            var mock = new Mock<IUserDataRepository>();
            Expression<Func<IUserDataRepository, UserData>> experssion = x => x.GetById(It.IsAny<string>());
            mock.Setup(experssion).Returns(new UserData() { BookProgress = "10:12 3:60" });
            
            _userDataRepository = mock.Object;

        }

        [Test]
        public void SaveBookProgressTest()
        { 
            var service = new TypingService(_bookRepository, _userDataRepository);
            var result = service.SaveBookProgress(3, 77, "guid");//CHYBA JEST ŹLE - DO POPRAWY SERWIS

            Assert.AreEqual("3:77 10:12", result);

        }
    }
}
