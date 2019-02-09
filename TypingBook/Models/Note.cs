using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypingBook.Models
{
    public class Note
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IdentityUser User { get; set; }
    }
}
