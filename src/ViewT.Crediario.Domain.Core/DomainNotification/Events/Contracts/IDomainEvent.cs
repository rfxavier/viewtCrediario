using System;

namespace ViewT.Crediario.Domain.Core.DomainNotification.Events.Contracts
{
    public interface IDomainEvent
    {
        DateTime Date { get; }
    }
}