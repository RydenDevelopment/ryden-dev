using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ryden_dev.Website.Enums;
using ryden_dev.Website.Filters;
using ryden_dev.Website.Models;

namespace ryden_dev.Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("{language}/")]
    public IActionResult Index()
    {
        if (LanguageCode == LanguageCodeEnum.En)
        {
            
        }
        else
        {
            
        }
        
        var model = new IndexViewModel();
        
        return View(model);
    }
    
    [Route("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("/error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}