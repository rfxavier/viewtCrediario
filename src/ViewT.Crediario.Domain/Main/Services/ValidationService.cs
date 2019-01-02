using System;
using ViewT.Crediario.Domain.Core.Commands;
using ViewT.Crediario.Domain.Core.DomainNotification;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;

namespace ViewT.Crediario.Domain.Main.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IPersonRepository _personRepository;

        public ValidationService(
            IPersonRepository personRepository
)
        {
            _personRepository = personRepository;
        }

        protected internal string SerialKey { get; private set; }
        protected internal string CondoId { get; private set; }


        private Person _person;

        public Person GetPerson()
        {
            return _person;
        }

        private void SetPerson(Person value)
        {
            _person = value;
        }

        public bool Validate(ICommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(command, Resources.Messages.InvalidCommandRequested));
        }
    }
}