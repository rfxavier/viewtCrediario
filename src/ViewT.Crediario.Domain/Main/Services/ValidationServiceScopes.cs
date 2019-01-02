using System;
using ViewT.Crediario.Domain.Core.DomainNotification;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Main.Services
{
    public static class ValidationServiceScopes
    {
        public static bool CommandNotNull(this ValidationService command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(command, Resources.Messages.InvalidCommandRequested));
        }

        #region .:: SerialKey validations ::.
        public static bool HasSerialKeyNotNull(this ValidationService command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(command.SerialKey, Resources.Messages.SerialKeyRequired));
        }

        public static bool HasSerialKeyNotEmpty(this ValidationService command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotEmpty(command.SerialKey, Resources.Messages.SerialKeyRequired));
        }

        public static bool HasSerialKey(this ValidationService command)
        {
            return HasSerialKeyNotNull(command) && HasSerialKeyNotEmpty(command);
        }
        #endregion


        public static bool HasFoundPerson(this ValidationService command, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(person, Resources.Messages.UserNotFound));
        }

    }
}