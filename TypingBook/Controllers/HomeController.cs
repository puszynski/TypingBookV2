using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;
using System.Linq;
using TypingBook.Data;
using TypingBook.Extensions;
using TypingBook.Helpers;
using TypingBook.Models;
using TypingBook.Repositories.IReporitories;
using TypingBook.ViewModels.Home;

namespace TypingBook.Controllers
{
    public class HomeController : BaseController
    {   
        readonly IBookRepository _bookRepository;
        readonly IMemoryCache _memoryCache;

        readonly IAgreementRepository _agreementRepository; // only for test filtering

        private const int _defaultBook = 2;

        public HomeController(IBookRepository bookRepository, IAgreementRepository agreementRepository, IMemoryCache memoryCache)
        {
            _bookRepository = bookRepository;
            _agreementRepository = agreementRepository;
            _memoryCache = memoryCache;
        }

        // Typing/Index is default route
        public string Index()
        {
            return "Ops - that route is out of date!";
        }

        public IActionResult Privacy()
        {
            bool isAjaxCall = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            
            if (isAjaxCall)
                return PartialView("_Privacy");
            else
                return View();
        }

        public IActionResult ModalPrivacy()
        {
            // do obsługi modala
            return View("ModalPrivacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// TODELETE - ONLY FOR TESTING FILTERING
        /// </summary>
        /// <returns></returns>
        public IActionResult AgreementsFilterTesting()
        {
            var agreements = _agreementRepository.GetAllAgreements();

            var filtered = FilterByDate(agreements, DateTime.Parse("2019-04-01"), DateTime.Parse("2019-05-01"));
            filtered = FilterByNonProlongedAgreements(filtered, agreements);

            return Json(filtered);

            // local function
            IQueryable<Agreement> FilterByDate(IQueryable<Agreement> query, DateTime from, DateTime to)
            {
                from = from.ToReportStartDate();
                to = to.ToReportEndDate();

                return query.Where(x => x.SignedDate >= from && x.SignedDate < to);
            }
            IQueryable<Agreement> FilterByNonProlongedAgreements(IQueryable<Agreement> query, IQueryable<Agreement> allAgreements)
            {
                // simplyfied - no terminantion date
                query.Where(q => allAgreements.Where
                                    (a => a.From <= q.To && a.To > q.To).Count() > 0);

                //query.Where(q => allAgreements.Where
                //                    (a => a.TerminationDate.HasValue && a.From <= q.To && a.TerminationDate > q.To
                //                    ||
                //                    !a.TerminationDate.HasValue && a.From <= q.To && a.To > q.To).Count() == 0);

                return query;
            }
        }
    }
}
