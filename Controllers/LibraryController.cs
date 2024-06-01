using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library_Management_System.context;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;

namespace Library_Management_System.Controllers
{
    // [Route("[controller]")]
    public class LibraryController : Controller
    {
        private readonly LibraryDbContext dbContext;
        public LibraryController(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public IActionResult UserHome()
        {
            return View();
        }

        public IActionResult LibrarianHome()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserBookCatalog()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> LibrarianBookCatalog()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookView viewModel)
        {
            var book = new Book
            {
                Title = viewModel.Title,
                Author = viewModel.Author,
                Genre = viewModel.Genre,
                ISBN = viewModel.ISBN,
                Copies = viewModel.Copies
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("LibrarianBookCatalog", "Library");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}