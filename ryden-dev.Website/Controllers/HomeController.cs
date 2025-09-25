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
        var model = new ContactViewModel();
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/")]
    public IActionResult Index(ContactViewModel model)
    {
        var isSuccess = false;
        TempData["AnchorValue"] = "contact";
        
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        // Check model so it contains data
        if (string.IsNullOrEmpty(model.Email) || 
            string.IsNullOrEmpty(model.Name) || 
            string.IsNullOrEmpty(model.Message))
        {
            TempData["result"] = isSuccess.ToString().ToLower();
            TempData["message"] = "Please fill out all required fields";
            return View(model);
        }

        try
        {
            // Prepare the contact form in to an email
            var notifyMessage = new NotifyMessage().PrepareContentFrom(model);
            var emailMessage = _notifyService.PrepareEmailFrom(notifyMessage);

            // Try and send the email
            isSuccess = _notifyService.SendMessage(emailMessage);
            TempData["result"] = isSuccess.ToString().ToLower();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error trying to send email");
        }

        if (isSuccess)
        {
            TempData["message"] = "We have received your message and will be in touch shortly!";
            return View(new ContactViewModel());
        }
        else
        {
            var contactEmail = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT");
            TempData["message"] = "We experienced a technical issue, please try and contact us on: " + contactEmail;
            return View(model);
        }
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