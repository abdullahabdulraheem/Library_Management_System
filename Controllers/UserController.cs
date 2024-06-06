using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library_Management_System.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private LibraryDbContext dbContext;
        public UserController(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult UserHome()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserBookCatalog()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> UserBookDetails(Guid id)
        {
            var books = await dbContext.Books.FindAsync(id);
            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}