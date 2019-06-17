using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypingBook.Models
{
    public class User : IdentityUser
    {
        public List<Agreement> Agreements { get; set; }
    }
}
