using System;
using ViewT.Crediario.Domain.Core.DomainNotification.Events.Contracts;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Events
{
    public class UserForgotPasswordRequestedEvent : IDomainEvent
    {
        public UserForgotPasswordRequestedEvent(Person person)
        {
            Person = person;
            Date = DateTime.Now;
        }
        public Person Person { get; private set; }
        public DateTime Date { get; }
    }
}