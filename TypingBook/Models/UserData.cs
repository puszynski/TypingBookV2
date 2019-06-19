using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TypingBook.Models.IModels;

namespace TypingBook.Models
{
    /// <summary>
    /// Additional User Data
    /// </summary>
    public class UserData : IId
    {
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        
        // e.g. 2:35 4:12 7:188 => Dictionary<BookId,BookPage> => last saved book go first
        public string BookProgress { get; set; }
        // e.g. 2018-09-11:87 2018-09-12:92 => Dictionary<DateTime,CorrectPercentage>
        public string Statistics { get; set; } // czy osobno dla kazdej ksiazki? 

        // TODO in future => a:97 b:87 c:87 => Character:CorrectPercentage
        // public string CharactersStatistics { get; set; }

    }
}
