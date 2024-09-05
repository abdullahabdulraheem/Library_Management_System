using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library_Management_System.Controllers;

[Authorize]
public class HomeController : Controller
{
    

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
