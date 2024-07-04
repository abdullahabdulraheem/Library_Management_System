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

        public async Task<IActionResult> Borrow()
        {
            var currentUserDetails = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);
            var libView = new LibrarianRequestView();
            libView.UserName = currentUserDetails.userName;
            libView.RequestMessage = $"{libView.UserName} requested to borrow";
            return RedirectToAction("UserBookCatalog", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}