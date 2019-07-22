using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TypingBook.Models;

namespace TypingBook.Controllers
{
    public class HomeController : BaseController
    {   
        // Typing/Index is default route
        public string Index()
        {
            return "Ops - that route is out of date!";
        }

        public IActionResult Privacy()
        {
            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            
            if (isAjaxCall)
                return PartialView("_Privacy");
            else
                return View();
        }

        public IActionResult ModalPrivacy()
        {
            // do obsługi modala
            return View("ModalPrivacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
