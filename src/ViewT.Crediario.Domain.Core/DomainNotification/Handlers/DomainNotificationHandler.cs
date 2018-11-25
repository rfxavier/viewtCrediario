using System.Collections.Generic;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;

namespace ViewT.Crediario.Domain.Core.DomainNotification.Handlers
{
    public class DomainNotificationHandler : IDomainNotificationHandler<Events.DomainNotification>
    {
        private List<Events.DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<Events.DomainNotification>();
        }

        public void Handle(Events.DomainNotification args)
        {
            _notifications.Add(args);
        }

        public IEnumerable<Events.DomainNotification> Notify()
        {
            return GetValue();
        }

        List<Events.DomainNotification> IDomainNotificationHandler<Events.DomainNotification>.Notify()
        {
            return GetValue();
        }

        public bool HasNotifications()
        {
            return GetValue().Count > 0;
        }

        public void Dispose()
        {
            _notifications = new List<Events.DomainNotification>();
        }

        private List<Events.DomainNotification> GetValue()
        {
            return _notifications;
        }
    }
}