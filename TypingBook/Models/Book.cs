using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TypingBook.Models.IModels;

namespace TypingBook.Models
{
    public class Book : IId
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(100)]
        public string Authors { get; set; }

        [Range(1, 10)]
        public int? Rate { get; set; }

        public int? Genre { get; set; } // binary sum

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public DateTime AddDate { get; set; }
                
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string License { get; set; }

        [DefaultValue(false)]
        public bool IsVerified { get; set; }
    }
}
