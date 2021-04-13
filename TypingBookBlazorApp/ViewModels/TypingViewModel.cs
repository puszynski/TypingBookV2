using System.Collections.Generic;

namespace TypingBookBlazorApp.ViewModels
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
