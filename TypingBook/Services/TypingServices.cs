using TypingBook.Helpers;
using TypingBook.ViewModels.Home;

namespace TypingBook.Services
{
    public class TypingServices
    {
        public TypingViewModel GetIntroductionModel(int currentBookPage = 0)
        {
            var introduction = "TypingBook is simple but smarth app that will rise yours quick and accuracy typing on keyboard and english words knowleadge - rewritng is Key here. Those skills will help you on many areas. But in the same time you can relax by typing your favorite books or use one of recomended. Skill up learn and take a fun! Switch to Light Layout if you prefer.<array> Use minimise option to size TypingWindow to minimum so you can e.g. watch movie in the same time.<arrow> By default we will use cookies to save your actuall progress on your computer. You have access to our library of public books. <arrow to BooksLibrary> If you need more - add your own books or load your notes to rewrite it (its briliant excersize) or get more advance statistisc - please LogIN so we can store your data in our Data Base.<arrow to LogIN> Now please enjoy the journey with default book - IN TO THE WILD by Jon Krakauer.";

            var typingHelper = TypingHelper.GetInstance();
            var introductionPagesList = typingHelper.DivideBook(introduction);

            return new TypingViewModel()
            {
                BookAuthors = "Web author",
                CurrentBookPage = currentBookPage,
                BookPages = introductionPagesList,
                BookTitle = "Introduction",
                BookID = 0
            };
        }

        public TypingViewModel GetTypingBookModel(string userId, int? defaultBook, int? bookPage)
        {

            // TODO pobranie progresu usera
            var lastTypedBookId = 2;
            var lastTypedBookCurrentPage = 3;


            // TODO pobranie book

            // TODO VM
            return null;
        }
    }
}
