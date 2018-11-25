using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;

namespace ViewT.Crediario.Infra.Data.RepositoryFake
{
    public class EmailNotificationFakeRepository : IEmailNotificationRepository
    {
        public EmailNotification Add(EmailNotification emailNotification)
        {
            return new EmailNotificationBuilder();
        }
    }
}
