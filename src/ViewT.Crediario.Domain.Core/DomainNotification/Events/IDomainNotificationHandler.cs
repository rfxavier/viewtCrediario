using System.Collections.Generic;
using ViewT.Crediario.Domain.Core.DomainNotification.Events.Contracts;

namespace ViewT.Crediario.Domain.Core.DomainNotification.Events
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T: IDomainEvent
    {
        List<T> Notify();
        bool HasNotifications();
    }
}
