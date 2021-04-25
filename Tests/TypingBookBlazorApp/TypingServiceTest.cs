using NSubstitute;
using System.Threading.Tasks;
using TypingBookBlazorApp.Data;
using TypingBookBlazorApp.Data.Repositories;
using TypingBookBlazorApp.Services;
using Xunit;

namespace Tests.TypingBookBlazorApp
{

    public class TypingServiceTest
    {
        TypingService _sut;

        [Fact]
        public void ShouldReturnIntroductionModel_ForNotLoggedUser()
        {
            //arrange
            var bookRepository = Substitute.For<BookRepository>();
            bookRepository.GetPublicByIDAsync(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(new Book() { }));

            _sut = new TypingService(bookRepository);

            //act
            var result =_sut.GetAsync(null).Result;

            //assert
            Assert.Equal("Introduction", result.BookTitle);
        }
    }
}
