namespace Pepegro.Bll.Services.MainServices.MailService;

using System.Net;
using System.Net.Mail;

public class MailService
{
    public MailService()
    {
        
    }

    public void SendEmail()
    {
        var client = new SmtpClient("smtp.gmail.com", 465)
        {
            Credentials = new NetworkCredential("nazariiturchynocych@gmail.com", "Nazarrava1"),
            EnableSsl = true
        };
        client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
        Console.WriteLine("Sent");
        Console.ReadLine();
    }
}