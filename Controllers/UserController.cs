using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System.Context;
using Library_Management_System.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Library_Management_System.Models;
using Library_Management_System.Data;

namespace Library_Management_System.Controllers
{
    // [Route("[controller]")]
    public class UserController(
        UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    LibraryDbContext dbContext,
    IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly LibraryDbContext _dbContext = dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        

        public IActionResult UserHome()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserBookCatalog()
        {
            var books = await _dbContext.Books.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> UserBookDetails(int id)
        {
            var books = await _dbContext.Books.FindAsync(id);
            return View(books);
        }

        public async Task<IActionResult> Borrow(int bookId)
        {
            var currentUserDetails = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);
            var books = await _dbContext.Books.FindAsync(bookId);
            var currentUser = await _dbContext.Users.FindAsync(currentUserDetails.userId);
            string librarianMessage = $"{currentUser!.UserName} requested to borrow {books!.Title}";
            string userMessage = $"You requested to borrow {books!.Title}";
            var borrowing = new Borrowing();
            borrowing.UserId = currentUserDetails.userId;
            borrowing.BorrowDate = DateTime.Now;
            borrowing.Book = books;
            borrowing.User = currentUser;
            borrowing.LibrarianMessage = librarianMessage;
            borrowing.UserMessage = userMessage;
            await _dbContext.Borrowings.AddAsync(borrowing);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("UserBookCatalog", "User");
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