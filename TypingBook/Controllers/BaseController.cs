using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TypingBook.Controllers
{
    public class BaseController : Controller
    {
        protected string GetLoggedUserId() 
            => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
