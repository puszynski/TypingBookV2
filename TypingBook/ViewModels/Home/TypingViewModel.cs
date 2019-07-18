using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypingBook.ViewModels.Home
{
    public class TypingViewModel
    {
        public int BookId { get; set; }

        // BookContent devided into list
        public List<string> BookPages { get; set; }

        public string BookTitle { get; set; }
        public string BookAuthors { get; set; }
        public int CurrentBookPage { get; set; }
    }
}
