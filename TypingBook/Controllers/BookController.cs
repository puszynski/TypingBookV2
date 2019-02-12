﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TypingBook.Data;
using TypingBook.Enums;
using TypingBook.Extensions;
using TypingBook.ViewModels.Book;

namespace TypingBook.Controllers
{
    public class BookController : Controller
    {
        readonly ISQLiteDapperRepository _sqLiteDB;

        public BookController(ISQLiteDapperRepository sqLiteDB) => _sqLiteDB = sqLiteDB;

        public IActionResult Index(string bookOrCompanySearchString)
        {
            var sql = _sqLiteDB.GetAllBooks();

            if (!string.IsNullOrWhiteSpace(bookOrCompanySearchString))
                sql = sql.Where(x => x.Title.Contains(bookOrCompanySearchString)
                                || x.Authors.Contains(bookOrCompanySearchString));

            var enumConv = new BinarySumToIntList();

            var row = sql.Select(x => new BookRowViewModel {
                ID = x.ID,
                Title = x.Title,
                Content = x.Content.ShowOnly500Char(),
                Authors = x.Authors,
                Genre = x.Genre.HasValue ? enumConv.Parse(x.Genre.GetValueOrDefault()).ToList() : null,
                Rate = x.Rate,
                ReleaseDate = x.ReleaseDate
            });

            var model = new BookViewModel(row);
            
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new BookViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var sql = _sqLiteDB.GetBookByID(id);

            if (sql == null)
                return View(); // TODO

            var enumConv = new BinarySumToIntList();

            var model = new BookRowViewModel
            {
                ID = sql.ID,
                Title = sql.Title,
                Content = sql.Content,
                Genre = sql.Genre.HasValue ? enumConv.Parse(sql.Genre.GetValueOrDefault()).ToList() : null,
                Authors = sql.Authors,
                Rate = sql.Rate,
                ReleaseDate = sql.ReleaseDate
            };
            return View(model);
        }
    }
}
