using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TypingBook.Enums;

namespace TypingBook.Models
{
    public class Agreement
    {
        public int ID { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime SignedDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public DateTime? TerminationDate { get; set; }
        public ETerminationType? TerminationType { get; set; }
    }
}
