using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Library_Management_System.Data.Context;
using Library_Management_System.Dto.User;
using Library_Management_System.Models;
using Library_Management_System.Service.Interface;
using Library_Management_System.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library_Management_System.Controllers;
public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequestDto request)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.UserLogin(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View();
        }

        return View();
    }


    [HttpGet("register-user")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> Register(CreateUserRequestDto request)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.UserRegistration(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        return View();
    }
}