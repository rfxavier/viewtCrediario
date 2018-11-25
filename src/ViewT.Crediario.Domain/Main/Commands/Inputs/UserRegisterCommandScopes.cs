using ViewT.Crediario.Domain.Core.DomainNotification;
using ViewT.Crediario.Domain.Main.Interfaces;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public static class UserRegisterCommandScopes
    {
        public static bool HasEmailNotNull(this UserRegisterCommand userRegisterCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(userRegisterCommand.Email, Resources.Messages.EmailRequired));
        }

        public static bool HasProperEmail(this UserRegisterCommand userRegisterCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertEmailIsValid(userRegisterCommand.Email, Resources.Messages.EmailProper));
        }

        public static bool HasValidEmail(this UserRegisterCommand userRegisterCommand)
        {
            return HasEmailNotNull(userRegisterCommand) && HasProperEmail(userRegisterCommand);
        }



        public static bool HasPasswordNotNull(this UserRegisterCommand userRegisterCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(userRegisterCommand.Password, Resources.Messages.UserRegisterPasswordRequired));
        }

        public static bool HasProperPassword(this UserRegisterCommand userRegisterCommand)
        {
            //todo proper password policy
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertIsGreaterThan(userRegisterCommand.Password.Length, 2, Resources.Messages.UserRegisterPasswordProper));
        }

        public static bool HasValidPassword(this UserRegisterCommand userRegisterCommand)
        {
            return HasPasswordNotNull(userRegisterCommand) && HasProperPassword(userRegisterCommand);
        }

        public static bool HasUniqueUserEmail(this UserRegisterCommand userRegisterCommand, IPersonRepository personRepository)
        {
            var person = personRepository.GetByEmail(userRegisterCommand.Email);
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertIsNull(person, Resources.Messages.UserRegisterEmailAlreadyTaken));
        }
    }
}