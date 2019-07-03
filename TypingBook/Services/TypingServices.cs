using TypingBook.Helpers;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;
using TypingBook.ViewModels.Home;

namespace TypingBook.Services
{
    public class TypingServices : ITypingServices
    {
        readonly IBookRepository _bookRepository;
        readonly IUserDataRepository _userDataRepository;
        readonly TypingHelper _typingHelper;
        readonly UserDataHelper _userDataHelper;

        public TypingServices(IBookRepository bookRepository, IUserDataRepository userDataRepository)
        {
            _bookRepository = bookRepository;
            _userDataRepository = userDataRepository;
            _typingHelper = TypingHelper.GetInstance();
            _userDataHelper = new UserDataHelper();
        }

        public TypingViewModel GetIntroductionModel(int? bookId, int? currentBookPage)
        {
            if(!bookId.HasValue || bookId == 0)
            {
                var introduction = "TypingBook is simple but smarth app that will rise yours quick and accuracy typing on keyboard and english words knowleadge - rewritng is Key here. Those skills will help you on many areas. But in the same time you can relax by typing your favorite books or use one of recomended. Skill up learn and take a fun! Switch to Light Layout if you prefer.<array> Use minimise option to size TypingWindow to minimum so you can e.g. watch movie in the same time.<arrow> By default we will use cookies to save your actuall progress on your computer. You have access to our library of public books. <arrow to BooksLibrary> If you need more - add your own books or load your notes to rewrite it (its briliant excersize) or get more advance statistisc - please LogIN so we can store your data in our Data Base.<arrow to LogIN> Now please enjoy the journey with default book - IN TO THE WILD by Jon Krakauer.";

                return new TypingViewModel()
                {
                    BookAuthors = "Web author",
                    CurrentBookPage = currentBookPage ?? 0,
                    BookPages = _typingHelper.DivideBook(introduction),
                    BookTitle = "Introduction",
                    BookID = 0
                };
            }

            return GetTypingViewModel(bookId, currentBookPage);
        }

        public TypingViewModel GetTypingBookModel(string userId, int? bookId, int? currentBookPage)
        {
            if (bookId.HasValue)
                return GetTypingViewModel(bookId, currentBookPage);
                        
            var userData = _userDataRepository.GetById(userId);

            if (userData == null)
                return GetTypingViewModel(bookId, currentBookPage);

            var lastTypedBookId = _userDataHelper.GetLastBookId(userData.Statistics);
            var lastTypedBookCurrentPage = _userDataHelper.GetLastCurrentBookPage(userData.Statistics); ;
            
            return GetTypingViewModel(lastTypedBookId, lastTypedBookCurrentPage);
        }

        private TypingViewModel GetTypingViewModel(int? bookId, int? currentBookPage)
        {
            var model = _bookRepository.GetBookByID(bookId.Value);

            return new TypingViewModel()
            {
                BookAuthors = model.Authors,
                CurrentBookPage = currentBookPage ?? 0,
                BookPages = _typingHelper.DivideBook(model.Content),
                BookTitle = model.Title,
                BookID = model.Id
            };
        } 
    }
}
