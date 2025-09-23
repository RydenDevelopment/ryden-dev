using Microsoft.AspNetCore.Mvc;

namespace ryden_dev.Website.Models;

public class ContactViewModel
{
    [BindProperty]
    public string? ContactType { get; set; }
    
    [BindProperty]
    public string? Name { get; set; }
    
    [BindProperty]
    public string? Email { get; set; }
    
    [BindProperty]
    public string? PhoneNumber { get; set; }
    
    [BindProperty]
    public string? Message { get; set; }
}