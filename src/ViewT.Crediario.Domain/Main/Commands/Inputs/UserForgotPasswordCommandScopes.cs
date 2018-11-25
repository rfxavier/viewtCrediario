using ViewT.Crediario.Domain.Core.DomainNotification;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public static class UserForgotPasswordCommandScopes
    {
        public static bool HasEmailNotNull(this UserForgotPasswordCommand userForgotPasswordCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertNotNull(userForgotPasswordCommand.Email, Resources.Messages.UserForgotPasswordEmailRequired));
        }

        public static bool HasProperEmail(this UserForgotPasswordCommand userForgotPasswordCommand)
        {
            return AssertionConcern.IsSatisfiedBy(AssertionConcern.AssertEmailIsValid(userForgotPasswordCommand.Email, Resources.Messages.UserForgotPasswordEmailProper));
        }

        public static bool HasValidEmail(this UserForgotPasswordCommand userForgotPasswordCommand)
        {
            return HasEmailNotNull(userForgotPasswordCommand) && HasProperEmail(userForgotPasswordCommand);
        }
    }
}