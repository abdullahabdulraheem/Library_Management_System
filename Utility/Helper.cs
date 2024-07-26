using Microsoft.AspNetCore.Identity;
using Library_Management_System.Data.Entities;

namespace Library_Management_System.Utility;

public static class Helper
{
    public static async Task<(string userId, string userName)> GetCurrentUserIdAsync(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if(httpContext!.User?.Identity?.IsAuthenticated == true)
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            return (user!.Id, user!.FirstName + user.LastName);
        }

        return (string.Empty, string.Empty);
    }
}