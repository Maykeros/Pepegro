namespace Pepegro.Bll.Services.MainServices.MailService;

using System.Net;
using System.Net.Mail;
using Domain.Entities.MailServiceEntities;
using Microsoft.Extensions.Options;
using MailMessage = System.Net.Mail.MailMessage;

public class MailService : IMailService
{
    private readonly MailCredentials _mailCredentials;

    public MailService(IOptions<MailCredentials> mailCredentials)
    {
        _mailCredentials = mailCredentials.Value;
    }

    public async void SendEmail(MailInformation mailInformation)
    {
        var client = new SmtpClient("smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("5d28710bc1862d", "f48c2dbd23fddd"),
            EnableSsl = true
        };
       await client.SendMailAsync(_mailCredentials.UserName, mailInformation.To, mailInformation.Subject, mailInformation.Message);
        
    }
}