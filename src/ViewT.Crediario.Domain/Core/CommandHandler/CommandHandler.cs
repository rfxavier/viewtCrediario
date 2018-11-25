using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Core.Interfaces;

namespace ViewT.Crediario.Domain.Core.CommandHandler
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IDomainNotificationHandler<DomainNotification.Events.DomainNotification> _notifications;

        public CommandHandler(IUnitOfWork uow, IDomainNotificationHandler<DomainNotification.Events.DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = notifications;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            return false;
        }
    }
}