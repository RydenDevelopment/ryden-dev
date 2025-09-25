using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ryden_dev.Website.Models;
using ryden_dev.Website.Services.Interface;
using ryden_dev.Website.Services.NotifyService;

namespace ryden_dev.Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INotifyService _notifyService;

    public HomeController(ILogger<HomeController> logger, INotifyService notifyService)
    {
        _logger = logger;
        _notifyService = notifyService;
    }

    [Route("/")]
    public IActionResult Index()
    {
       
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/")]
    public IActionResult Index(ContactViewModel model)
    {
        var isSuccess = false;
        
        if (!ModelState.IsValid)
        {
            TempData["result"] = isSuccess.ToString().ToLower();
            TempData["message"] = "Please verify that you are not a bot.";
            return View();
        }
        
        // Check model so it contains data
        if (model.Email == null || model.ContactType == null || model.Message == null)
            return View();

        // Prepare the contact form in to an email
        var notifyMessage = new NotifyMessage().PrepareContentFrom(model);
        var emailMessage = _notifyService.PrepareEmailFrom(notifyMessage);
        
        // Try and send the email
        isSuccess = _notifyService.SendMessage(emailMessage);

        // Returns the result of sending the message to the user
        var contactEmail = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT");
        TempData["result"] = isSuccess.ToString().ToLower();
        TempData["message"] = isSuccess ? 
            "We have received your message and will be in touch shortly!" : 
            "We experienced a technical issue, please try and contact us on: " + contactEmail;

        return View();
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