using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Library_Management_System.Views.Library
{
    public class UserBookDetails : PageModel
    {
        private readonly ILogger<UserBookDetails> _logger;

        public UserBookDetails(ILogger<UserBookDetails> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}