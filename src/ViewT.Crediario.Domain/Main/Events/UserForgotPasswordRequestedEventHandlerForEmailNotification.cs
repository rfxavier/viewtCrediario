using System;
using ViewT.Crediario.Domain.Core.DomainNotification;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;

namespace ViewT.Crediario.Domain.Main.Events
{
    public class UserForgotPasswordRequestedEventHandlerForEmailNotification : IHandler<UserForgotPasswordRequestedEvent>
    {
        private readonly IEmailNotificationRepository _notificationRepository;

        public UserForgotPasswordRequestedEventHandlerForEmailNotification(IEmailNotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public void Handle(UserForgotPasswordRequestedEvent args)
        {
            var emailNotification = new EmailNotification(Guid.NewGuid(), "emailFrom", args.Person.Email, "subject",
                "Condomínio - Nova senha\n\nPara entrar você deve usar a seguinte senha temporária: " + args.Person.Password +
                " . Lembre-se de redefinir sua senha.", string.Empty, DateTime.Now);

            _notificationRepository.Add(emailNotification);
        }
    }
}