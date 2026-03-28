using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ryden_dev.Website.Models;

public class ContactViewModel
{
    [BindProperty]
    public string? ContactType { get; set; }
    
    [BindProperty]
    public string? ProductName { get; set; }
    
    [BindProperty]
    [Required]
    public string? Name { get; set; }
    
    [BindProperty]
    public string? LastName { get; set; } //Hidden field for bots
    
    [BindProperty]
    [Required]
    public string? Email { get; set; }
    
    [BindProperty]
    public string? PhoneNumber { get; set; }
    
    [BindProperty]
    [Required]
    public string? Message { get; set; }
}