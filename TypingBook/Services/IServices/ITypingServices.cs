using TypingBook.ViewModels.Typing;

namespace TypingBook.Services.IServices
{
    public interface ITypingServices
    {
        TypingViewModel GetTypingViewModel(string userId, int? bookId, int? currentBookPage);

        /// <summary>
        /// Returns userData.BookProgress string when success or null when error
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="nextBookPage"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string SaveBookProgress(int bookId, int nextBookPage, string userId);
    }
}