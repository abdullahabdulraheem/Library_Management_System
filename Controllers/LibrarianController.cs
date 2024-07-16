using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library_Management_System.Context;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;
using Library_Management_System.Data;
using Microsoft.AspNetCore.Identity;
using Library_Management_System.Data.Enum;

namespace Library_Management_System.Controllers
{
    // [Route("[controller]")]
    public class LibrarianController(
        UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    LibraryDbContext dbContext,
    IHttpContextAccessor httpContextAccessor
    ) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly LibraryDbContext _dbContext = dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public IActionResult LibrarianHome()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LibrarianBookCatalog()
        {
            var books = await _dbContext.Books.ToListAsync();
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
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("LibrarianBookCatalog", "Librarian");
        }

        public async Task<IActionResult> Assign(int borrowId)
        {
            var borrowing = await _dbContext.Borrowings.FindAsync(borrowId);
            borrowing!.Status = Status.Approved;
            borrowing.Book.Copies -= 1;
            return View();
        }

        public async Task<IActionResult> Dismiss(int borrowId)
        {
            var borrowing = await _dbContext.Borrowings.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == borrowId);
            if(borrowing is not null)
            {
                _dbContext.Remove(borrowing);
                await _dbContext.SaveChangesAsync();
            }

            return View();
        }

        public async Task<IActionResult> Requests()
        {
            var borrowings = await _dbContext.Borrowings.ToListAsync();
            return View(borrowings);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}