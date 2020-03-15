using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TypingBook.Enums;
using TypingBook.Helpers;

namespace TypingBook.ViewModels.Book
{
    public class BookRowViewModel
    {
        public BookRowViewModel()
        {
            GenreSelectListItem = CreateSelectListItemHelper.GetInstance().GetSelectListItems<EBookGenre>();
        }


        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Book Content")]
        public string Content { get; set; }

        [Display(Name = "Book content before modyfications(display only)")]
        public string ContentBeforeModification { get; set; }

        [Display(Name = "Book content after formating(display only)")]
        public string ContentFormated { get; set; }

        public List<string> ContentInBookPages
        {
            get
            {
                try
                {
                    return JsonSerializer.Deserialize<List<string>>(Content);
                }
                catch (Exception)
                {
                    return null;
                }
            }            
            set
            {
                Content = JsonSerializer.Serialize(value);
            }
        }

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
        public DateTime AddDate { get; set; }
        public string UserId { get; set; }
        public string License { get; set; }
        public bool IsVerified { get; set; }
    }
}
