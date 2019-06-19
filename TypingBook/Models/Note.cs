using Microsoft.AspNetCore.Identity;
using TypingBook.Models.IModels;

namespace TypingBook.Models
{
    public class Note : IId
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IdentityUser User { get; set; }
    }
}
