using ViewT.Crediario.Domain.Main.Commands.Inputs;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders
{
    public class UserForgotPasswordCommandBuilder
    {
        private string _email = null;

        public UserForgotPasswordCommand Build()
        {
            var authenticateUserCommand = new UserForgotPasswordCommand(_email);

            return authenticateUserCommand;
        }

        public UserForgotPasswordCommandBuilder WithEmail(string email)
        {
            this._email = email;
            return this;
        }

        public static implicit operator UserForgotPasswordCommand(
            UserForgotPasswordCommandBuilder instance)
        {
            return instance.Build();
        }

    }
}