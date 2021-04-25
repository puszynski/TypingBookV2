using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TypingBookBlazorApp.Data.Interfaces;

namespace TypingBookBlazorApp.Data
{
    public class Book : IId
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// string contains JSON witch is array of string (book pages)
        /// </summary>
        [Required]
        public string Content { get; set; }

        public string ContentBeforeModifying { get; set; }

        //[Required]
        //public string[] BookPages { get; set; }

        [Required]
        [MaxLength(100)]
        public string Authors { get; set; }

        public string Description { get; set; }

        [Range(1, 10)]
        public int? Rate { get; set; }

        // binary sum
        public int? Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public DateTime AddDate { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string License { get; set; }

        [DefaultValue(false)]
        public bool IsVerified { get; set; }// only !IsPrivate can be verified and then are public

        public bool IsPrivate { get; set; } = false;
    }
}
