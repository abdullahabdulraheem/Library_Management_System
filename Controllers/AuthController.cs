using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Library_Management_System.Context;
using Library_Management_System.Models;
using Library_Management_System.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library_Management_System.Controllers;
public class AuthController(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    INotyfService notyfService,
    LibraryDbContext libraryDbContext,
    IHttpContextAccessor httpContextAccessor) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly INotyfService _notyfService = notyfService;
    private readonly LibraryDbContext _libraryDbContext = libraryDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);

            var result = await _signInManager.PasswordSignInAsync(user!.UserName!, model.Password, false, lockoutOnFailure: false);

            // var result = await _signInManager.PasswordSignInAsync(user!.UserName!, model.Password, false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                var redirectResult = RedirectToAction("Index", "Home");

                _notyfService.Success("You're Logged in successfully");
                return redirectResult;
            }
            
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            _notyfService.Error("Invald Login Attempt");
            return View(model);
        }

        return View(model);
    }

    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(ModelState.IsValid)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.Username);
            if(existingUser != null)
            {
                _notyfService.Warning("User already exist!");
                return View();
            }

            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                _notyfService.Error("An error occured");
                return View();
            }

            _notyfService.Success("Registration was successful");
            await _signInManager.SignInAsync(user, isPersistent : false);
            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }
    
    public IActionResult Index()
    {
        return View();
    }


    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View("Error!");
    // }
}