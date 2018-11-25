using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Interfaces
{
    public interface IEmailNotificationRepository
    {
        EmailNotification Add(EmailNotification emailNotification);
    }
}