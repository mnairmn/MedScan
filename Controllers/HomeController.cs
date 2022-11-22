using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MedScan.Models;

namespace MedScan.Controllers;

public class HomeController : Controller //inherits from Controller class
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    
    public IActionResult Index()
    {
        return View();
    }

    [BasicAuthorize] //calling of the BasicAuthorizeAttribute, limiting access
    public IActionResult Login()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

