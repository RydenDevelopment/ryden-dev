using System.Net;
using System.Net.Mail;
using ryden_dev.Website.Services.Interface;
using static System.Net.ServicePointManager;

namespace ryden_dev.Website.Services.NotifyService;

public class EmailService : INotifyService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;
    
    /// <summary>
    /// Set SMTP variables from Environmental variables on creation.
    /// </summary>
    public EmailService()
    {
        _host = Environment.GetEnvironmentVariable("SMTP_HOST") ?? string.Empty;
        _port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0");
        _username = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? string.Empty;
        _password = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? string.Empty;
        
        SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
    }

    /// <summary>
    /// Set custom variables for the SMTP service on creation.
    /// </summary>
    /// <param name="host">Set the SMTP Host</param>
    /// <param name="port">Set the SMTP Port</param>
    /// <param name="username">Set the SMTP Username</param>
    /// <param name="password">Set the SMTP Password</param>
    public EmailService(string host, int port, string username, string password)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
    }
    
    /// <summary>
    /// Uses a MailMessage object to send an email with SMTP.
    /// Environmental Variables need to be set for SMTP before use.
    /// </summary>
    /// <param name="mailMessage"></param>
    /// <returns>Boolean if the message was sent or encountered an error</returns>
    public bool SendMessage(MailMessage mailMessage)
    {
        // Creating an SMTP client and setting its properties
        using var mailClient = new SmtpClient(_host, _port);
        mailClient.EnableSsl = true;
        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        mailClient.Credentials = new NetworkCredential(_username, _password);

        // Trying to send the message, throwing exception if it fails
        try
        {
            mailClient.Send(mailMessage);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine("Message was not sent: " + ex);
            return false;
        }
        finally
        {
            mailMessage.Dispose();
        }

        return true;
    }

    /// <summary>
    /// Converts a INotifyMessage to a MailMessage for use with the SMTP EmailService.
    /// </summary>
    /// <param name="notifyMessage">Pass an INotifyMessage object</param>
    /// <returns>An MailMessage object</returns>
    public MailMessage PrepareEmailFrom(INotifyMessage notifyMessage)
    {
        if (string.IsNullOrEmpty(notifyMessage.From))
            throw new ArgumentException("The SMTP environmental variables has not been set properly");
        
        if (string.IsNullOrEmpty(notifyMessage.Recipient))
            throw new ArgumentException("The recipient email has not ben set");
        
        // Set a new MailMessage object with the values from the INotifyMessage object
        var message =
            new MailMessage(
                notifyMessage.From, 
                notifyMessage.Recipient, 
                notifyMessage.Subject, 
                notifyMessage.Message);

        return message;
        
        /*
        // Returns the message if there are no attachments
        if (notifyMessage.Attachments.Count <= 0)
           return message;

        // Loop the number of attachments and add them to the message
        // OBS - This is not in use yet and has not been tested
        for (var index = 0; index < notifyMessage.Attachments.Count; index++)
        {
            var notifyMessageAttachment = notifyMessage.Attachments[index];
            message.Attachments.Add(
                new Attachment(
                    notifyMessageAttachment,
                    $"image{index + 1}.jpeg",
                    "image/jpeg"));
        }

        return message;
        */
    }
}