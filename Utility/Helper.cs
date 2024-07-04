using Microsoft.AspNetCore.Identity;

namespace Library_Management_System.Utility;

public static class Helper
{
    public static async Task<(string userId, string userName)> GetCurrentUserIdAsync(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if(httpContext!.User?.Identity?.IsAuthenticated == true)
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            return (user!.Id, user!.UserName!);
        }

        return (string.Empty, string.Empty);
    }
}