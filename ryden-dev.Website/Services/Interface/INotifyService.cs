using System.Net.Mail;

namespace ryden_dev.Website.Services.Interface;

public interface INotifyService
{
    public bool SendMessage(MailMessage mailMessage);

    public MailMessage PrepareEmailFrom(INotifyMessage notifyMessage);
}