using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserChangePasswordCommand : ICommand
    {
        public UserChangePasswordCommand(string identification, string serialKey, string oldPassword, string newPassword)
        {
            Identification = identification;
            SerialKey = serialKey;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string Identification { get; private set; }
        public string SerialKey { get; private set; }
        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
    }
}