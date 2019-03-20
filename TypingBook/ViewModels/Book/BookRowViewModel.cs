using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypingBook.ViewModels.Book
{
    public class BookRowViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Book Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Book Authors")]
        public string Authors { get; set; }

        [Range(1, 10)]
        public int? Rate { get; set; }

        [Display(Name = "Book Genre")]
        public List<int> Genre { get; set; }

        public IEnumerable<SelectListItem> GenreSelectListItem { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Relase Date")]
        public DateTime? ReleaseDate { get; set; }
    }
}
