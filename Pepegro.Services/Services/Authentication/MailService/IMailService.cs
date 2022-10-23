namespace Pepegro.Bll.Services.MainServices.MailService;

using Domain.Entities.MailServiceEntities;

public interface IMailService
{
    public void SendEmail(MailInformation mailInformation);
}