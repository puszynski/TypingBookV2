using TypingBook.ViewModels.Home;

namespace TypingBook.Services.IServices
{
    public interface ITypingServices
    {
        TypingViewModel GetIntroductionModel(int? bookId, int? currentBookPage);
        TypingViewModel GetTypingBookModel(string userId, int? bookId, int? currentBookPage);
    }
}