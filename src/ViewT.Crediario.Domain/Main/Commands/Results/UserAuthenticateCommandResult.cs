using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Results
{
    public class UserAuthenticateCommandResult : ICommandResult
    {
        public string Name { get; set; }
        public string SerialKey { get; set; }
        public string PushToken { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public bool Visitor { get; set; }
        public bool Resident { get; set; }
        public string Document { get; set; }
    }
}