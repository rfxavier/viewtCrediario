using AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using ViewT.Condominio.Domain.Core.DomainNotification.Events;
using ViewT.Condominio.Domain.Main.Commands.Handlers;
using ViewT.Condominio.Domain.Main.Interfaces;
using ViewT.Condominio.Domain.Tests.Unit.Main.Commands.Builders;
using ViewT.Condominio.Domain.Tests.Unit.Main.Entities.Builders;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Main.Commands.Handlers;
using ViewT.Crediario.Domain.Main.Commands.Inputs;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;
using Xunit;

namespace ViewT.Condominio.Domain.Tests.Unit.Main.Commands.Handlers
{
    public class UserChangePasswordCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();


        public UserChangePasswordCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "UserChangePasswordCommandHandler Handle invalid not from repository")]
        [Trait("Category", "UserChangePasswordCommandHandler")]
        public void
            UserChangePasswordCommand_WithInvalidPropertiesNotFromRepositories_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var invalidCommand = new UserChangePasswordCommandBuilder()
                .WithSerialKey(null);

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(invalidCommand);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserChangePasswordIdentificationRequired)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.SerialKeyRequired);
        }

        [Fact(DisplayName = "UserChangePasswordCommandHandler Handle invalid from repository - Person not found")]
        [Trait("Category", "UserChangePasswordCommandHandler")]
        public void
            UserChangePasswordCommand_WithUserNotFound_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var command = (UserChangePasswordCommand) new UserChangePasswordCommandBuilder()
                .WithSerialKey(Guid.NewGuid().ToString());

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetBySerialKey(command.SerialKey))
                .Returns(() => null);

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(command);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserNotFound);

            _mocker.GetMock<IPersonRepository>().Verify(u => u.GetBySerialKey(It.IsAny<string>()),Times.Once());
            _mocker.GetMock<IDeviceRepository>().Verify(u => u.GetByDeviceIdentificationIncludingPerson(It.IsAny<string>()),Times.Once());
        }


        [Fact(DisplayName = "UserChangePasswordCommandHandler Handle invalid from repository - Device not found")]
        [Trait("Category", "UserChangePasswordCommandHandler")]
        public void
            UserChangePasswordCommand_WithDeviceNotFound_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var command = (UserChangePasswordCommand) new UserChangePasswordCommandBuilder()
                .WithSerialKey(Guid.NewGuid().ToString());

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetBySerialKey(command.SerialKey))
                .Returns(new PersonBuilder());

            //_mocker.GetMock<IDeviceRepository>()
            //    .Setup(d => d.GetByDeviceIdentificationIncludingPerson(command.Identification))
            //    .Returns(() => null);

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(command);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserChangePasswordDeviceNotFound);

            _mocker.GetMock<IPersonRepository>().Verify(u => u.GetBySerialKey(It.IsAny<string>()),Times.Once());
            _mocker.GetMock<IDeviceRepository>().Verify(u => u.GetByDeviceIdentificationIncludingPerson(It.IsAny<string>()),Times.Once());
        }


        [Fact(DisplayName = "UserChangePasswordCommandHandler Handle invalid from repository - Device does not belong to Person")]
        [Trait("Category", "UserChangePasswordCommandHandler")]
        public void
            UserChangePasswordCommand_WithDeviceNotBelongingToUser_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var command = (UserChangePasswordCommand) new UserChangePasswordCommandBuilder()
                .WithSerialKey(Guid.NewGuid().ToString());

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetBySerialKey(command.SerialKey))
                .Returns(() => new PersonBuilder().WithPersonId(Guid.NewGuid()));

            //_mocker.GetMock<IDeviceRepository>()
            //    .Setup(d => d.GetByDeviceIdentificationIncludingPerson(command.Identification))
            //    .Returns(() => new DeviceBuilder().WithPerson(new PersonBuilder().WithPersonId(Guid.NewGuid())));

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(command);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserChangePasswordDeviceDoesNotBelongToUser);

            _mocker.GetMock<IPersonRepository>().Verify(u => u.GetBySerialKey(It.IsAny<string>()),Times.Once());
            _mocker.GetMock<IDeviceRepository>().Verify(u => u.GetByDeviceIdentificationIncludingPerson(It.IsAny<string>()),Times.Once());
        }


        [Fact(DisplayName = "UserChangePasswordCommandHandler Handle valid Device belongs to Person")]
        [Trait("Category", "UserChangePasswordCommandHandler")]
        public void
            UserChangePasswordCommand_WithDeviceBelongingToUser_Handle_ShouldReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var command = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder()
                .WithSerialKey(Guid.NewGuid().ToString());

            var userId = Guid.NewGuid();

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetBySerialKey(command.SerialKey))
                .Returns(() => new PersonBuilder().WithPersonId(userId));

            //_mocker.GetMock<IDeviceRepository>()
            //    .Setup(d => d.GetByDeviceIdentificationIncludingPerson(command.Identification))
            //    .Returns(() => new DeviceBuilder().WithPerson(new PersonBuilder().WithPersonId(userId)));

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => new PersonBuilder());

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(command);

            //Assert
            _notifications.Should()
                .BeEmpty();

            _mocker.GetMock<IPersonRepository>().Verify(u => u.GetBySerialKey(It.IsAny<string>()), Times.Once());
            _mocker.GetMock<IDeviceRepository>().Verify(u => u.GetByDeviceIdentificationIncludingPerson(It.IsAny<string>()), Times.Once());
        }



    }
}