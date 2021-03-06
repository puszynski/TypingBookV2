﻿using System;

namespace TypingBook.ViewModels
{
    public class BaseViewModel
    {
        #region paging
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage => (int)Math.Ceiling((decimal)TotalItems / (ItemsPerPage != 0 ? ItemsPerPage : 1));
        #endregion

        public string backUrl { get; set; }
    }
}
