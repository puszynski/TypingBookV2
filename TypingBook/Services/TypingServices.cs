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

        public TypingViewModel GetTypingViewModel(string userId, int? bookId, int? currentBookPage)
        {
            var result = new TypingViewModel();

            if (bookId.HasValue)
                result = GetTypingViewModelByBookId(bookId.Value, currentBookPage);
            else if (string.IsNullOrEmpty(userId))
                result = GetIntroductionModel();
            else
                result = GetModelForLoggedUser(userId);

            return result;
        }

        public TypingViewModel GetIntroductionModel()
        {
            var introduction = "TypingBook is simple but smarth app that will rise yours quick and accuracy typing on keyboard and english words knowleadge - rewritng is Key here. Those skills will help you on many areas. But in the same time you can relax by typing your favorite books or use one of recomended. Skill up learn and take a fun! Switch to Light Layout if you prefer.<array> Use minimise option to size TypingWindow to minimum so you can e.g. watch movie in the same time.<arrow> By default we will use cookies to save your actuall progress on your computer. You have access to our library of public books. <arrow to BooksLibrary> If you need more - add your own books or load your notes to rewrite it (its briliant excersize) or get more advance statistisc - please LogIN so we can store your data in our Data Base.<arrow to LogIN> Now please enjoy the journey with default book - IN TO THE WILD by Jon Krakauer.";

            return new TypingViewModel()
            {
                BookAuthors = "Web author",
                CurrentBookPage = 0,
                BookPages = _typingHelper.DivideBook(introduction),
                BookTitle = "Introduction",
                BookID = 0
            };
        }

        public TypingViewModel GetModelForLoggedUser(string userId)
        {            
            var userData = _userDataRepository.GetById(userId);

            //TODO
            //if (userData == null)
            //    CreateUserData()???;
            //if (string.IsNullOrEmpty(userData.BookProgress))
            //    // GetIntroduction?
            
            var lastTypedBookId = _userDataHelper.GetLastBookId(userData.BookProgress);
            var lastTypedBookCurrentPage = _userDataHelper.GetLastCurrentBookPage(userData.BookProgress);
                                   
            return GetTypingViewModelByBookId(lastTypedBookId.Value, lastTypedBookCurrentPage);
        }
        
        private TypingViewModel GetTypingViewModelByBookId(int bookId, int? currentBookPage)
        {
            var model = _bookRepository.GetBookByID(bookId);

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
