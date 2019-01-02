using System;
using System.Collections.Generic;
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
            var commandResult = new UserRegisterCommandResult();

            if (!(_validationService.Validate(command)))
            {
                return commandResult;
            }

            //Validações de coisas que não vão em repositório
            if (!(command.HasValidEmail() & command.HasValidPassword()))
            {
                return commandResult;
            }

            //Validações de coisas que vão em repositório
            if (!(command.HasUniqueUserEmail(_personRepository)))
            {
                return commandResult;
            }

            //Gera nova entidade
            var person = new Person(Guid.NewGuid(), command.Name, command.DocumentNumber, phoneNumber: command.PhoneNumber, email: command.Email,
                password: _passwordService.Encrypt(command.Password), serialKey: Guid.NewGuid().ToString().Replace("-", ""));

            //Adiciona as entidades ao repositório
            var personAdded = _personRepository.Add(person);

            commandResult.SerialKey = personAdded.SerialKey;

            return commandResult;
        }

        public ICommandResult Handle(UserAuthenticateCommand command)
        {
            var commandResult = new UserAuthenticateCommandResult();

            if (!(_validationService.Validate(command)))
            {
                return commandResult;
            }

            //Validações de coisas que não vão em repositório
            if (!(command.HasUserName() & command.HasPassword()))
                {

                return commandResult;
            }

            //Validações de coisas que vão em repositório
            var person = _personRepository.GetByEmailAndPassword(command.User, _passwordService.Encrypt(command.Password));

            if (!(command.HasFoundAuthorizedUser(person) && command.HasActiveUser(person)))
            {
                return commandResult;
            }

            //Trata fluxo demais regras
            person.SetSerialKey(Guid.NewGuid().ToString().Replace("-", ""));

            person.SetPushToken(command.PushToken);

            #region .: Comment :.
            //var personDevice = _deviceRepository.GetDeviceByPersonIncludingPerson(person);

            //if (personDevice == null)
            //{
            //    var newDevice = new Device(Guid.NewGuid(), description: "", deviceToken: command.Identification, pushToken: "", simCardNumber: "",
            //        deviceOs: DeviceOs.FromValue(command.DeviceOs), identification: command.Identification, person: person);

            //    _deviceRepository.Add(newDevice);
            //}
            //else
            //{
            //    if (personDevice.Identification != command.Identification)
            //    {
            //        personDevice.Disable();
            //        personDevice.Deactivate();
            //        _deviceRepository.Update(personDevice);

            //        var newDevice = new Device(Guid.NewGuid(), description: "", deviceToken: command.Identification, pushToken: "", simCardNumber: "",
            //            deviceOs: DeviceOs.FromValue(command.DeviceOs), identification: command.Identification, person: person);

            //        _deviceRepository.Add(newDevice);
            //    }
            //}

            //if (person.Token == null)
            //{
            //    var newToken = new Token(tokenId: Guid.NewGuid(), userToken: Guid.NewGuid(), deviceOs: DeviceOs.FromValue(command.DeviceOs));

            //    _tokenRepository.Add(newToken);

            //    person.SetToken(newToken);
            //}
            //else
            //{
            //    var token = person.Token;

            //    token.Deactivate();

            //    _tokenRepository.Update(token);


            //    var newToken = new Token(tokenId: Guid.NewGuid(), userToken: Guid.NewGuid(), deviceOs: DeviceOs.FromValue(command.DeviceOs));

            //    _tokenRepository.Add(newToken);

            //    person.SetToken(newToken);
            //}

            #endregion

            _personRepository.Update(person);

            IEnumerable<CondoPerson> condoPersonList = _condoPersonRepository.GetByPersonIdIncludingCondo(person.PersonId);

            var condos = condoPersonList.Select(x => new UserAuthenticateCommandResult.Condo()
            {
                CondoId = x.Condo.CondoId.ToString(),
                CondoName = x.Condo.Name,
                AddressApartment = x.AddressApartment,
                AddressBlock = x.AddressBlock,
                AddressComplement = x.AddressComplement,
                Admin = x.Admin,
                Visitor = false,
                Resident = x.Resident,
                ResidentCode = x.ResidentCode
            }).ToList();

            var personVisitorList = _personVisitorRepository.GetByPersonId(person.PersonId);

            foreach (var personVisitor in personVisitorList)
            {
                if (condos.All(c => c.CondoId != personVisitor.Condo.CondoId.ToString()))
                {
                    condos.Add(new UserAuthenticateCommandResult.Condo()
            {
                        CondoId = personVisitor.Condo.CondoId.ToString(),
                        CondoName = personVisitor.Condo.Name,
                        AddressApartment = string.Empty,
                        AddressBlock = string.Empty,
                        AddressComplement = string.Empty,
                        Admin = false,
                        Visitor = true,
                        Resident = false,
                        ResidentCode = string.Empty
                    });
            }
            else
            {
                    //condos.FirstOrDefault(c => c.CondoId == personVisitor.Condo.CondoId.ToString()).Visitor = true;
                }
            }

            return new UserAuthenticateCommandResult()
            {
                Name = person.Name,
                SerialKey = person.SerialKey,
                PushToken = person.PushToken,
                PhoneNumber = person.PhoneNumber,
                DocumentNumber = person.DocumentNumber,
                Email = person.Email,
                Condos = condos
            };
        }

        public ICommandResult Handle(UserForgotPasswordCommand command)
        {
            var commandResult = new UserForgotPasswordCommandResult();

            //Validações de coisas que não vão em repositório
            if (!(command.HasValidEmail()))
            {
                return commandResult;
            }

            //Gera nova entidade

            //Trata fluxo demais regras
            var person = _personRepository.GetByEmail(command.Email);

            if (person != null)
            {
                string plainNewPassword = Guid.NewGuid().ToString().Substring(0, 7);
                string password = _passwordService.Encrypt(plainNewPassword);
                person.SetPassword(password);

                _personRepository.Update(person);

                if (Commit())
                {
                    DomainEvent.Raise(new UserForgotPasswordRequestedEvent(person, plainNewPassword));

                    commandResult.SerialKey = person.SerialKey;
                }
            }

            return commandResult;
        }

        public ICommandResult Handle(UserChangePasswordCommand command)
        {
            var commandResult = new UserChangePasswordCommandResult();
            //Validações de coisas que não vão em repositório
            //todo .HasOldAndNewPassword()
            if (!(command.HasSerialKey()))
            {
                return commandResult;
            }

            //Validações de coisas que vão em repositório
            var personFoundBySerialKey = _personRepository.GetBySerialKey(command.SerialKey);
            //var device = _deviceRepository.GetByDeviceIdentificationIncludingPerson(command.Identification);

            //if (!(command.HasFoundPerson(personFoundBySerialKey) && command.HasFoundDevice(device) && command.HasFoundDeviceBelongsToPerson(device, personFoundBySerialKey)))
            if (!(command.HasFoundPerson(personFoundBySerialKey)))
            {
                return commandResult;
            }

            var personFoundByUsernameAndPassword =
                _personRepository.GetByEmailAndPassword(personFoundBySerialKey.Email, _passwordService.Encrypt(command.OldPassword));

            if (!(command.HasFoundAuthorizedUser(personFoundByUsernameAndPassword) && command.HasActiveUser(personFoundByUsernameAndPassword)))
                {
                return commandResult;
            }

            return commandResult;
            }

        public ICommandResult Handle(UserUpdatePushTokenCommand command)
        {
            var commandResult = new UserUpdatePushTokenCommandResult();

            if (!(_validationService.Validate(command)))
            {
                return commandResult;
            }

            if (!(command.HasSerialKey() & command.HasPushToken()))
                {
                return commandResult;
            }


            var person = _personRepository.GetBySerialKey(command.SerialKey);

            if (!(command.HasFoundPerson(person)))
            {
                return commandResult;
            }

            person.SetPushToken(command.PushToken);

            _personRepository.Update(person);

            return commandResult;
        }
    }
}