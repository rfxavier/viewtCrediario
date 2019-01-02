using ViewT.Crediario.Domain.Core.DomainNotification;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public static class UserChangePasswordCommandScopes
    {
        public static bool HasSerialKey(this UserChangePasswordCommand command)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(command.SerialKey, Resources.Messages.SerialKeyRequired));
        }

        public static bool HasFoundPerson(this UserChangePasswordCommand command, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(person, Resources.Messages.UserNotFound));
        }

        public static bool HasFoundDevice(this UserChangePasswordCommand command, Device device)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(device, Resources.Messages.UserChangePasswordDeviceNotFound));
        }

        public static bool HasFoundDeviceBelongsToPerson(this UserChangePasswordCommand command, Device device, Person person)
        {
            return command.HasFoundDevice(device)
                   && command.HasFoundPerson(person)
                   && AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertAreEquals(device.Person.PersonId.ToString(), person.PersonId.ToString(),
                       Resources.Messages.UserChangePasswordDeviceDoesNotBelongToUser));
        }

        public static bool HasFoundAuthorizedUser(this UserChangePasswordCommand authenticateUserCommand, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(person, Resources.Messages.UserAuthenticateLoginFailed));
        }

        public static bool HasActiveUser(this UserChangePasswordCommand authenticateUserCommand, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertTrue(person != null && person.PersonUserStatus == PersonUserStatus.Active, Resources.Messages.UserAuthenticateUserIsInactive));
        }

    }
}