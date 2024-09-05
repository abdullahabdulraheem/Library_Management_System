using Library_Management_System.Data.Entities;
using Library_Management_System.Dto.Book;
using Library_Management_System.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Library_Management_System.ViewComponents
{
    //Create a Class, and it should inherit from ViewComponent class
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly IBookService _bookService;
        private readonly UserManager<User> _userManager;

        public NotificationsViewComponent(IBookService bookService, UserManager<User> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }
        //The Invoke method for the View component
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var userName = User?.Identity?.Name;

            var notifications = new List<LibraryNotificationDto>() { };

            if (!string.IsNullOrEmpty(userName))
            {
                var loggedInUser = await _userManager.FindByNameAsync(userName);
                if (loggedInUser != null)
                {
                    var result = await _bookService.GetNotificationByUserIdSlim(loggedInUser.Id);

                    notifications = result.Data;
                }
                
            }
            return View(notifications);
        }
    }
}

