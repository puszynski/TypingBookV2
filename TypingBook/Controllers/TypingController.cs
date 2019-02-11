using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TypingBook.Controllers
{
    public class TypingController
    {
        //private const int _defaultBook = 2;
        //private readonly IBooksRepository _booksRepository;

        //public TypingController(IBooksRepository booksRepository)
        //{
        //    _booksRepository = booksRepository;
        //}


        //public IActionResult Index(int bookID = _defaultBook, int bookPage = 0)
        //{
        //    var book = _booksRepository.GetBookByID(bookID);

        //    if (bookID == 1)
        //        ViewBag.IsIntroduction = true;

        //    var typingHelper = new TypingHelper();
        //    var bookPages = typingHelper.DivideBook(book.BookContent);

        //    var authorsList = _booksRepository.GetAuthorsByBookID(bookID);

        //    var authorNamesHelper = new GetAuthorsFullNameListHelper();
        //    var bookAuthors = authorNamesHelper.Get(authorsList.ToList());

        //    var model = new TypingViewModel()
        //    {
        //        BookAuthors = bookAuthors,
        //        CurrentBookPage = bookPage,
        //        BookPages = bookPages,
        //        BookTitle = book.BookTitle,
        //        BookID = bookID
        //    };


        //    bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
        //    if (isAjaxCall)
        //        return PartialView("_Index", model);
        //    else
        //        return View(model);
        //}
    }
}
