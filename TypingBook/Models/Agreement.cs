using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TypingBook.Enums;
using TypingBook.Models.IModels;

namespace TypingBook.Models
{
    public class Agreement : IId
    {
        public int Id { get; set; }

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
