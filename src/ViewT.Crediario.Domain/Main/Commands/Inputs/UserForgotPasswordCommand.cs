using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserForgotPasswordCommand : ICommand
    {
        public UserForgotPasswordCommand(string email)
        {
            Email = email;
        }
        public string Email { get; private set; }
    }
}