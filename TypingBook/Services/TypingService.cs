using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TypingBook.Helpers;
using TypingBook.Repositories.IReporitories;
using TypingBook.Services.IServices;
using TypingBook.ViewModels.Typing;

namespace TypingBook.Services
{
    public class TypingService : ITypingServices
    {
        readonly IBookRepository _bookRepository;
        readonly IUserDataRepository _userDataRepository;
        readonly TypingHelper _typingHelper;
        readonly UserDataHelper _userDataHelper;

        public TypingService(IBookRepository bookRepository, IUserDataRepository userDataRepository)
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
                result = GetTypingViewModelByBookId(bookId.Value, currentBookPage, userId);
            else if (string.IsNullOrEmpty(userId))
                result = GetIntroductionModel();
            else
                result = GetModelForLoggedUser(userId);

            if (result == null)
                result = GetIntroductionModel();

            if (IsEndOfTheBook(result.CurrentBookPage, result.BookPages.Count))
                return null;

            return result;
        }

        bool IsEndOfTheBook(int currentPage, int bookPages)
        {
            return currentPage == bookPages ? true : false;
        }

        TypingViewModel GetIntroductionModel()
        {
            //todoChabgeToJSON
            var introduction = "[\"TypingBook is simple but smarth app that will rise yours quick and accuracy typing on keyboard and english words knowleadge - rewritng is Key here.\",\"Those skills will help you on many areas. But in the same time you can relax by typing your favorite books or use one of recomended.\",\"Skill up learn and take a fun! Switch to Light Layout if you prefer.\\u003Carray\\u003E Use minimise option to size TypingWindow to minimum so you can e.g.\",\"watch movie in the same time.\\u003Carrow\\u003E You can type any book we have but your progress will be not saved. Please log in to store your progress or just choose page manually.\"]";

            return new TypingViewModel()
            {
                BookAuthors = "Web author",
                CurrentBookPage = 0,
                BookPages = JsonSerializer.Deserialize<List<string>>(introduction),
                BookTitle = "Introduction",
                BookId = 0
            };
        }

        TypingViewModel GetModelForLoggedUser(string userId)
        {            
            var userData = _userDataRepository.GetById(userId);

            //TODO
            //if (userData == null)
            //    CreateUserData()???;
            //if (string.IsNullOrEmpty(userData.BookProgress))
            //    // GetIntroduction?
            
            var lastTypedBookId = _userDataHelper.GetLastBookId(userData.BookProgress);
            var lastTypedBookCurrentPage = _userDataHelper.GetLastCurrentBookPage(userData.BookProgress);
                                   
            return GetTypingViewModelByBookId(lastTypedBookId, lastTypedBookCurrentPage, userId);
        }
        
        TypingViewModel GetTypingViewModelByBookId(int? bookId, int? currentBookPage, string userId)
        {
            if (!bookId.HasValue || bookId.Value == 0)
                return null;

            var model = _bookRepository.GetBookByID(bookId.Value);

            if (!currentBookPage.HasValue)
                currentBookPage = TryGetCurrentBookPage(bookId.Value, userId);
            
            return new TypingViewModel()
            {
                BookAuthors = model.Authors,
                CurrentBookPage = currentBookPage ?? 0,
                BookPages = JsonSerializer.Deserialize<List<string>>(model.Content),
                BookTitle = model.Title,
                BookId = model.Id
            };
        }

        int? TryGetCurrentBookPage(int bookId, string userId)
        {
            var userBookProgress = _userDataRepository.GetById(userId).BookProgress;
            var currentBookPage = _userDataHelper.GetCurrentBookPageByBookId(userBookProgress, bookId);
            return currentBookPage;
        }

        public string SaveBookProgress(int bookId, int nextBookPage, string userId)
        {
            if (bookId == 0)
                return null; //no save for introduction

            var userData = _userDataRepository.GetById(userId);

            var bookProgress = _userDataHelper.DeserializeProgressBar(userData.BookProgress);
            bookProgress[bookId] = nextBookPage;
            userData.BookProgress = _userDataHelper.SerializeProgressBar(bookProgress, bookId);

            try
            {
                _userDataRepository.SaveChanges();
            }
            catch (System.Exception)
            {
                return null;
            }
            return userData.BookProgress;
        }
    }
}
