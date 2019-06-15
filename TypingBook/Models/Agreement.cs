using Microsoft.AspNetCore.Identity;
using System;
using TypingBook.Enums;

namespace TypingBook.Models
{
    public class Agreement
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public DateTime SignedDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public IdentityUser User { get; set; }

        public DateTime TerminationDate { get; set; }
        public ETerminationType TerminationType { get; set; }
    }
}
