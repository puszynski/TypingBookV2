using Moq;
using TypingBook.Models;

namespace Tests.Mock
{
    internal static class MockEntity
    {
        public static Book Book()
        {
            var mock = new Mock<Book>();
            mock.SetupProperty(p => p.Id, 1);
            mock.SetupProperty(p => p.Title, "Tytuł");
            mock.SetupProperty(p => p.Content, "Testowa treść książki.");
            mock.SetupProperty(p => p.Authors, "Jakub Puszyński");

            return mock.Object;
        }
    }
}
