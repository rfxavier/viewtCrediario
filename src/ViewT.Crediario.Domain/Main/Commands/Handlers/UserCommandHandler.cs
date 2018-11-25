using System;
using ViewT.Crediario.Domain.Core.CommandHandler;
using ViewT.Crediario.Domain.Core.Commands;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Core.Interfaces;
using ViewT.Crediario.Domain.Main.Commands.Inputs;
using ViewT.Crediario.Domain.Main.Commands.Results;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Enums;
using ViewT.Crediario.Domain.Main.Events;
using ViewT.Crediario.Domain.Main.Interfaces;

namespace ViewT.Crediario.Domain.Main.Commands.Handlers
{
    public class UserCommandHandler : CommandHandler,
        ICommandHandler<UserRegisterCommand>,
        ICommandHandler<UserAuthenticateCommand>,
        ICommandHandler<UserForgotPasswordCommand>,
        ICommandHandler<UserChangePasswordCommand>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITokenRepository _tokenRepository;

        public UserCommandHandler(
            IPersonRepository personRepository,
            IDeviceRepository deviceRepository,
            ITokenRepository tokenRepository,
            IUnitOfWork uow, IDomainNotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _personRepository = personRepository;
            _deviceRepository = deviceRepository;
            _tokenRepository = tokenRepository;
        }

        public ICommandResult Handle(UserRegisterCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.HasValidEmail() & command.HasValidPassword()))
            {
                return new UserRegisterCommandResult()
                {
                    UserId = Guid.Empty
                };
            }

            //Validações de coisas que vão em repositório
            if (!(command.HasUniqueUserEmail(_personRepository)))
            {
                return new UserRegisterCommandResult()
                {
                    UserId = Guid.Empty
                };
            }

            //Gera nova entidade
            //todo checar policy para password de new user (new auto generated password?)
            var person = new Person(Guid.NewGuid(), command.Name, command.DocumentNumber, phoneNumber: String.Empty, email: command.Email, password: command.Password, serialKey: Guid.NewGuid().ToString().Replace("-", ""));

            //Adiciona as entidades ao repositório
            var personAdded = _personRepository.Add(person);

            return new UserRegisterCommandResult()
            {
                UserId = personAdded.PersonId
            };
        }

        public ICommandResult Handle(UserAuthenticateCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.IsNotNull() && command.HasUserName() & command.HasPassword()))
            {
                return new UserAuthenticateCommandResult()
                {

                };
            }

            //Validações de coisas que vão em repositório
            var person = _personRepository.GetByUserNameAndPassword(command.User, command.Password);

            if (!(command.HasFoundAuthorizedUser(person) && command.HasActiveUser(person)))
            {
                return new UserAuthenticateCommandResult()
                {

                };
            }

            //Trata fluxo demais regras
            person.SetSerialKey(Guid.NewGuid().ToString().Replace("-", ""));

            var personDevice = _deviceRepository.GetByPerson(person);

            if (personDevice == null)
            {
                var newDevice = new Device(Guid.NewGuid(), description: "", deviceToken: command.Identification, pushToken: "", simCardNumber: "", deviceOs: DeviceOs.FromValue(command.DeviceOs), identification: command.Identification, person: person);

                _deviceRepository.Add(newDevice);
            }
            else
            {
                if (personDevice.Identification != command.Identification)
                {
                    personDevice.Disable();
                    personDevice.Deactivate();
                    _deviceRepository.Update(personDevice);

                    var newDevice = new Device(Guid.NewGuid(), description: "", deviceToken: command.Identification, pushToken: "", simCardNumber: "", deviceOs: DeviceOs.FromValue(command.DeviceOs), identification: command.Identification, person: person);

                    _deviceRepository.Add(newDevice);
                }
            }

            if (person.Token == null)
            {
                var newToken = new Token(tokenId: Guid.NewGuid(), userToken: Guid.NewGuid(), deviceOs: DeviceOs.FromValue(command.DeviceOs));

                _tokenRepository.Add(newToken);

                person.SetToken(newToken);

                _personRepository.Update(person);
            }
            else
            {
                var token = person.Token;

                token.Deactivate();

                _tokenRepository.Update(token);


                var newToken = new Token(tokenId: Guid.NewGuid(), userToken: Guid.NewGuid(), deviceOs: DeviceOs.FromValue(command.DeviceOs));

                _tokenRepository.Add(newToken);

                person.SetToken(newToken);

                _personRepository.Update(person);
            }

            return new UserAuthenticateCommandResult()
            {
                Name = person.Name,
                SerialKey = person.Token.UserToken.ToString(),
                PushToken = "",
                PhoneNumber = "",
                Document = person.DocumentNumber,
                Email = person.Email,
                Admin = person.Admin,
                Visitor = person.Visitor,
                Resident = person.Resident
            };
        }

        public ICommandResult Handle(UserForgotPasswordCommand command)
        {
            //Gera os Value Objects

            //Validações de coisas que não vão em repositório
            if (!(command.HasValidEmail()))
            {
                return new UserForgotPasswordCommandResult()
                {
                    SerialKey = string.Empty
                };
            }

            //Gera nova entidade

            //Trata fluxo demais regras
            var person = _personRepository.GetByEmail(command.Email);

            if (person != null)
            {
                person.SetPassword(Guid.NewGuid().ToString().Substring(0, 7));

                _personRepository.Update(person);

                if (Commit())
                {
                    DomainEvent.Raise(new UserForgotPasswordRequestedEvent(person));

                    return new UserForgotPasswordCommandResult
                    {
                        SerialKey = person.SerialKey
                    };
                }
            }

            //Adiciona as entidades ao repositório

            return new UserForgotPasswordCommandResult
            {
                SerialKey = string.Empty
            };
        }

        public ICommandResult Handle(UserChangePasswordCommand command)
        {
            //Validações de coisas que não vão em repositório
            if (!(command.HasIdentification() & command.HasSerialKey()))
            {
                return new UserChangePasswordCommandResult()
                {
                    SerialKey = string.Empty
                };
            }

            //Validações de coisas que vão em repositório
            var personFoundBySerialKey = _personRepository.GetBySerialKey(command.SerialKey);
            var device = _deviceRepository.GetByIdentification(command.Identification);

            if (!(command.HasFoundPerson(personFoundBySerialKey) && command.HasFoundDevice(device) && command.HasFoundDeviceBelongsToPerson(device, personFoundBySerialKey)))
            {
                return new UserChangePasswordCommandResult()
                {
                    SerialKey = string.Empty
                };
            }

            var personFoundByUsernameAndPassword = _personRepository.GetByUserNameAndPassword(personFoundBySerialKey.Email, command.OldPassword);

            if (!(command.HasFoundAuthorizedUser(personFoundByUsernameAndPassword) && command.HasActiveUser(personFoundByUsernameAndPassword)))
            {
                return new UserChangePasswordCommandResult()
                {
                    SerialKey = string.Empty
                };
            }


            //Trata fluxo demais regras


            return new UserChangePasswordCommandResult()
            {
                SerialKey = string.Empty
            };
        }
    }
}