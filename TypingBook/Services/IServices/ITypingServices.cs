using TypingBook.ViewModels.Home;

namespace TypingBook.Services.IServices
{
    public interface ITypingServices
    {
        TypingViewModel GetTypingViewModel(string userId, int? bookId, int? currentBookPage);

        bool SaveBookProgress(string userId, int bookId, int typedBookPage);
    }
}