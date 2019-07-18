using System.Collections.Generic;

namespace TypingBook.ViewModels.Typing
{
    public class TypingViewModel
    {
        public int BookId { get; set; }
        public List<string> BookPages { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthors { get; set; }
        public int CurrentBookPage { get; set; }
    }
}
