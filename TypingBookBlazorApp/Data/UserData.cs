using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TypingBookBlazorApp.Data.Interfaces;

namespace TypingBookBlazorApp.Data
{
    /// <summary>
    /// Additional Identity User Data, refers on UserId(GUID)
    /// </summary>
    public class UserData : IId
    {
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }


        // e.g. 2:35 4:12 7:188 => Dictionary<BookId,BookPage> => last saved book go first
        public string BookProgress { get; set; }

        // JSON string => Tuple<DateTime,CorrectTypedCount,WrongTypedCount,SecondOfTyping> // w jakiej jednoste liczysz szybkość?
        public string Statistics { get; set; } // czy osobno dla kazdej ksiazki?

    }
}
