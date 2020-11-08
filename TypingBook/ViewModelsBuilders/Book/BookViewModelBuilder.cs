using System;
using System.Collections.Generic;
using System.Linq;
using TypingBook.Enums;
using TypingBook.Extensions;
using TypingBook.Helpers;
using TypingBook.Repositories.IReporitories;
using TypingBook.ViewModels.Book;

namespace TypingBook.ViewModelsBuilders.Book
{
    public class BookViewModelBuilder : IViewModelBuilder<BookViewModel>
    {
        readonly IBookRepository _bookRepository;
        readonly IUserDataRepository _userDataRepository;

        readonly string _userId;
        readonly bool _isLoggerdUserAdministrator;
        readonly string _bookOrAuthorSearchString;
        readonly int? _genreFilter;

        public BookViewModelBuilder(IBookRepository bookRepository, 
                                    IUserDataRepository userDataRepository,
                                    string userId,
                                    bool isLoggerdUserAdministrator,
                                    string bookOrAuthorSearchString, 
                                    int? genreFilter)
        {
            _bookRepository = bookRepository;
            _userDataRepository = userDataRepository;
            _userId = userId;
            _isLoggerdUserAdministrator = isLoggerdUserAdministrator;
            _bookOrAuthorSearchString = bookOrAuthorSearchString;
            _genreFilter = genreFilter;
        }

        public BookViewModel Build()
        {
            var sql = _bookRepository.GetAllBooksAvailableForUser(_userId, _isLoggerdUserAdministrator);

            if (!string.IsNullOrWhiteSpace(_bookOrAuthorSearchString))
                sql = sql.Where(x => x.Title.Contains(_bookOrAuthorSearchString)
                                || x.Authors.Contains(_bookOrAuthorSearchString));

            if (_genreFilter.HasValue && _genreFilter != 0)
                sql = sql.Where(x => (x.Genre & _genreFilter) > 0);

            var bookRowViewModels = sql
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Authors,
                    x.Genre,
                    x.Rate,
                    x.ReleaseDate,
                    x.AddDate,
                    x.IsVerified,
                    x.License
                })
                .ToList()
                .Select(x => new BookRowViewModel
                {
                    ID = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Authors = x.Authors,
                    Genre = x.Genre.HasValue ? x.Genre.Value.ConvertEnumSumToIntArray().ToList() : null,
                    Rate = x.Rate,
                    ReleaseDate = x.ReleaseDate,
                    AddDate = x.AddDate,
                    IsVerified = x.IsVerified,
                    License = x.License
                })
                .ToList();

            AssignUserLastTypedPage(bookRowViewModels, _userId);

            var model = new BookViewModel(bookRowViewModels);
            model.BookGenreSelectListItems = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();

            // TODO - if ajax load partial view like in Home/Index
            return model;
        }

        public BookViewModel Rebuild()
        {
            throw new NotImplementedException();
        }

        private void AssignUserLastTypedPage(List<BookRowViewModel> bookRowViewModels, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return;

            var userBooksInProgress = _userDataRepository.GetByIdUserLastTypedPages(userId);

            foreach (var item in bookRowViewModels)
                item.UserLastTypedPage = userBooksInProgress.SingleOrDefault(x => x.bookID == item.ID).userLastPage;
        }
    }
}
