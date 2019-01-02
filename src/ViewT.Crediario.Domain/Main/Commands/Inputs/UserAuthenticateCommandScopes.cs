using ViewT.Crediario.Domain.Core.DomainNotification;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Enums;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public static class UserAuthenticateCommandScopes
    {
        //todo tests IsNotNull
        public static bool IsNotNull(this UserAuthenticateCommand userAuthenticateCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(userAuthenticateCommand, Resources.Messages.InvalidCommandRequested));
        }
        public static bool HasUserName(this UserAuthenticateCommand userAuthenticateCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(userAuthenticateCommand.User, Resources.Messages.UserAuthenticateUserRequired));
        }

        public static bool HasPassword(this UserAuthenticateCommand userAuthenticateCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(userAuthenticateCommand.Password, Resources.Messages.UserAuthenticatePasswordRequired));
        }

        public static bool HasFoundAuthorizedUser(this UserAuthenticateCommand userAuthenticateCommand, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(person, Resources.Messages.UserAuthenticateLoginFailed));
        }

        public static bool HasActiveUser(this UserAuthenticateCommand userAuthenticateCommand, Person person)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertTrue(person != null && person.PersonUserStatus.Value == PersonUserStatus.Active.Value, Resources.Messages.UserAuthenticateUserIsInactive));
        }
    }
}