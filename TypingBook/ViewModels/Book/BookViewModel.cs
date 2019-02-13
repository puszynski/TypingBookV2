using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace TypingBook.ViewModels.Book
{
    public class BookViewModel : IEnumerable<BookRowViewModel>
    {
        protected IEnumerable<BookRowViewModel> sqlQuery;

        public BookViewModel()
        {

        }

        public BookViewModel(IEnumerable<BookRowViewModel> query)
        {
            sqlQuery = query;

            if (query == null)
                return;
        }

        public IEnumerable<SelectListItem> BookGenreSelectListItems { get; set; }

        public IEnumerator<BookRowViewModel> GetEnumerator()
        {
            return sqlQuery.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
