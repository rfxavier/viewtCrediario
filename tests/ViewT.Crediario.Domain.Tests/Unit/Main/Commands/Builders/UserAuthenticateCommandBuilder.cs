using ViewT.Crediario.Domain.Main.Commands.Inputs;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders
{
    public class UserAuthenticateCommandBuilder
    {
        private string _user = null;
        private string _password = null;
        private string _identification = null;
        private int _deviceOs = 1;
        private string _deviceModel = null;
        private string _versionOs = null;

        public UserAuthenticateCommand Build()
        {
            var authenticateUserCommand = new UserAuthenticateCommand(_user, _password, _identification, _deviceOs, _deviceModel, _versionOs);

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

        public UserAuthenticateCommandBuilder WithIdentification(string identification)
        {
            this._identification = identification;
            return this;
        }

        public static implicit operator UserAuthenticateCommand(
            UserAuthenticateCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}