using System;
using ViewT.Crediario.Domain.Core.DomainNotification.Events.Contracts;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Events
{
    public class UserForgotPasswordRequestedEvent : IDomainEvent
    {
        public UserForgotPasswordRequestedEvent(Person person, string plainNewPassword)
        {
            Person = person;
            PlainNewPassword = plainNewPassword;
            Date = DateTime.Now;
        }
        public Person Person { get; private set; }
        public string PlainNewPassword { get; private set; }
        public DateTime Date { get; }
    }
}