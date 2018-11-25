using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Main.Commands.Inputs
{
    public class UserRegisterCommand : ICommand
    {
        public UserRegisterCommand(string name, string email, string phoneNumber, string password, string documentNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            DocumentNumber = documentNumber;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public string DocumentNumber { get; private set; }
    }
}