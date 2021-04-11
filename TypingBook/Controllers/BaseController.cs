using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TypingBook.Controllers
{
    public class BaseController : Controller
    {
        protected string GetLoggedUserId() 
            => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        protected bool IsLoggerdUserAdministrator()
            => HttpContext.User.IsInRole("Administrator");

        protected string ErrorMessage
        {
            get { return (string)TempData["ErrorMessage"]; }
            set { TempData["ErrorMessage"] = value; }
        }
    }
}
