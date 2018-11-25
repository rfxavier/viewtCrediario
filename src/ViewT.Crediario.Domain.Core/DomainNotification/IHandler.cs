using ViewT.Crediario.Domain.Core.DomainNotification.Events.Contracts;

namespace ViewT.Crediario.Domain.Core.DomainNotification
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}