using System.Text;
using ryden_dev.Website.Models;
using ryden_dev.Website.Services.Interface;

namespace ryden_dev.Website.Services.NotifyService;

public class NotifyMessage : INotifyMessage
{
    public string From { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<MemoryStream> Attachments { get; set; } = [];

    /// <summary>
    /// Creates a INotifyMessage object from a ContactViewModel object for use with a message service.
    /// </summary>
    /// <param name="contactViewModel"></param>
    /// <returns>An INotifyMessage object</returns>
    /// <exception cref="ArgumentException">Throws an exception if Email, ContactType or Message is null</exception>
    public INotifyMessage PrepareContentFrom(ContactViewModel contactViewModel)
    {
        // Check model so it contains data
        if (contactViewModel.Email == null || 
            contactViewModel.ContactType == null || 
            contactViewModel.Message == null)
            throw new ArgumentException("Data was not provided in the ContactViewModel");
        
        // Set its variables from the model
        From = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT") ?? string.Empty;
        Recipient = Environment.GetEnvironmentVariable("SMTP_CONTACT_RECIPIENT") ?? string.Empty;
        Subject = "Rydén.dev Form: "+ contactViewModel.ContactType;
        
        // Setting the message body with the contact information
        var builder = new StringBuilder();
        builder.Append($"Namn: {contactViewModel.Name}{Environment.NewLine}");
        
        builder.Append($"Email: {contactViewModel.Email}{Environment.NewLine}");
        
        // Add phone-number and address if they exist
        if (contactViewModel.PhoneNumber != null)
            builder.Append($"Telefon: {contactViewModel.PhoneNumber}{Environment.NewLine}");

        if (contactViewModel.ContactType == "Services" && !string.IsNullOrEmpty(contactViewModel.ProductName))
            builder.Append($"Valt paket: {contactViewModel.ProductName}{Environment.NewLine}");
        
        builder.Append($"{Environment.NewLine}" +
                       $"Meddelande:{Environment.NewLine}" +
                       $"{contactViewModel.Message}{Environment.NewLine}");

        Message = builder.ToString();

        return this;
    }
}