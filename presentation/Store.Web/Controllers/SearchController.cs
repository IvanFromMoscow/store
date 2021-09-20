using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBookRepository booksRepository;

        public SearchController(IBookRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }
        public IActionResult Index(string query)
        {
            var books = booksRepository.GetAllBookByTitle(query);
            return View(books);
        }
    }
}