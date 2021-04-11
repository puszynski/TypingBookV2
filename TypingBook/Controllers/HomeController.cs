using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using TypingBook.Models;

namespace TypingBook.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var fooTxt = "   1,  2,3 ,4,  5,6   ";
            var t = Regex.Replace(fooTxt, @"\s+", "");
            var t1 = t.Split(',').Select(int.Parse).ToList();

            //var t2 = fooTxt.Trim();
            var t22 = fooTxt.Split(new char[] { ',', ';' }).Select(x => int.Parse(x)).ToList();

            return RedirectToAction("Index", "Book", "");
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
