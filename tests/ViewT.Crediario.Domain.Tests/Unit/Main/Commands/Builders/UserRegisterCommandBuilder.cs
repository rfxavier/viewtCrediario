using System;
using ViewT.Crediario.Domain.Main.Commands.Inputs;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders
{
    public class UserRegisterCommandBuilder
    {
        private string _name = string.Empty;
        private string _email = String.Empty;
        private string _password = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _documentNumber = string.Empty;

        public UserRegisterCommand Build()
        {
            var device = new UserRegisterCommand(_name,_email,_phoneNumber,_password,_documentNumber);

            return device;
        }

        public UserRegisterCommandBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }

        public UserRegisterCommandBuilder WithEmail(string email)
        {
            this._email = email;
            return this;
        }

        public UserRegisterCommandBuilder WithPassword(string password)
        {
            this._password = password;
            return this;
        }

        public UserRegisterCommandBuilder WithPhoneNumber(string phoneNumber)
        {
            this._phoneNumber = phoneNumber;
            return this;
        }

        public UserRegisterCommandBuilder WithDocumentNumber(string documentNumber)
        {
            this._documentNumber = documentNumber;
            return this;
        }

        public static implicit operator UserRegisterCommand(UserRegisterCommandBuilder instance)
        {
            return instance.Build();
        }
    }
}