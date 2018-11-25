using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Results
{
    public class UserForgotPasswordCommandResult : ICommandResult
    {
        public string SerialKey { get; set; }
    }
}