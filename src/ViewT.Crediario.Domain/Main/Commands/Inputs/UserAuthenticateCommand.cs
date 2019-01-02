using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserAuthenticateCommand : ICommand
    {
        public UserAuthenticateCommand(string user, string password, string pushToken)
        {
            User = user;
            Password = password;
            PushToken = pushToken;
        }

        public string User { get; private set; }
        public string Password { get; private set; }
        public string PushToken { get; private set; }
    }
}