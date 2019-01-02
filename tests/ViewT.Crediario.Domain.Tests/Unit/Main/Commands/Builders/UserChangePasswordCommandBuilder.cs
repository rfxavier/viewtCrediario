using ViewT.Crediario.Domain.Main.Commands.Inputs;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders
{
    public class UserChangePasswordCommandBuilder
    {
        private string _serialKey = null;
        private string _oldPassword = null;
        private string _newPassword = null;

        public UserChangePasswordCommand Build()
        {
            var authenticateUserCommand = new UserChangePasswordCommand(_serialKey, _oldPassword, _newPassword);

            return authenticateUserCommand;
        }

        public UserChangePasswordCommandBuilder WithSerialKey(string serialKey)
        {
            this._serialKey = serialKey;
            return this;
        }

        public UserChangePasswordCommandBuilder WithOldPassword(string oldPassword)
        {
            this._oldPassword = oldPassword;
            return this;
        }

        public UserChangePasswordCommandBuilder WithNewPassword(string newPassword)
        {
            this._newPassword = newPassword;
            return this;
        }


        public static implicit operator UserChangePasswordCommand(
            UserChangePasswordCommandBuilder instance)
        {
            return instance.Build();
        }


    }
}