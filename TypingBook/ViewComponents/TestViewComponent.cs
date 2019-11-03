using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;

namespace TypingBook.ViewComponents
{
    //https://docs.microsoft.com/pl-pl/aspnet/core/mvc/views/view-components?view=aspnetcore-3.0
    public class TestViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public TestViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(
        int maxPriority, bool isDone)
        {
            var items = await GetItemsAsync(maxPriority, isDone);
            return View(items);
        }
        private Task<List<Book>> GetItemsAsync(int maxPriority, bool isDone)
        {
            return _bookRepository.GetAllBooksAsync();
        }
    }
}
