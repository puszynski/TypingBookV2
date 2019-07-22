using TypingBook.ViewModels.Typing;

namespace TypingBook.Services.IServices
{
    public interface ITypingServices
    {
        TypingViewModel GetTypingViewModel(string userId, int? bookId, int? currentBookPage);
        bool SaveBookProgress(int bookId, int nextBookPage, string userId);
    }
}