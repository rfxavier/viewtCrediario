using ViewT.Crediario.Domain.Main.Commands.Inputs;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders
{
    public class UserAuthenticateCommandBuilder
    {
        private string _user = null;
        private string _password = null;
        private string _pushToken = null;

        public UserAuthenticateCommand Build()
        {
            var authenticateUserCommand = new UserAuthenticateCommand(_user, _password, _pushToken);

            return authenticateUserCommand;
        }

        public UserAuthenticateCommandBuilder WithUser(string user)
        {
            this._user = user;
            return this;
        }

        public UserAuthenticateCommandBuilder WithPassword(string password)
        {
            this._password = password;
            return this;
        }

        public UserAuthenticateCommandBuilder WithPushToken(string pushToken)
        {
            this._pushToken = pushToken;
            return this;
        }

        public static implicit operator UserAuthenticateCommand(
            UserAuthenticateCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}