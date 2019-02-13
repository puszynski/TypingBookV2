using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TypingBook.Models
{
    public class Book
    {
        public int ID { get; set; }

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
    }
}
