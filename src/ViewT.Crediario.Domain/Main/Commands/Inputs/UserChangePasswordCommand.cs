using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserChangePasswordCommand : ICommand
    {
        public UserChangePasswordCommand(string serialKey, string oldPassword, string newPassword)
        {
            SerialKey = serialKey;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string SerialKey { get; private set; }
        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
    }
}