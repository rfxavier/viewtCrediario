using AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Main.Commands.Inputs;
using ViewT.Crediario.Domain.Main.Enums;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;
using Xunit;

namespace ViewT.Condominio.Domain.Tests.Unit.Main.Commands.Scopes
{
    public class UserCommandScopesTests
    {
        private readonly AutoMoqer _mocker;

        public UserCommandScopesTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        [Fact(DisplayName = "HasValidEmail Success")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithValidEmail_HasValidEmail_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithEmail("contato@viewt.com.br");

            //Act


            //Assert
            registerNewUserCommand.HasValidEmail().Should().BeTrue();

            _notifications.Should().BeEmpty();
        }

        [Fact(DisplayName = "HasValidEmail Null")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithNullEmail_HasValidEmail_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithEmail(null);

            //Act


            //Assert
            registerNewUserCommand.HasValidEmail().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.EmailRequired);
        }

        [Fact(DisplayName = "HasValidEmail Empty")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithEmptyEmail_HasValidEmail_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithEmail("");

            //Act


            //Assert
            registerNewUserCommand.HasValidEmail().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.EmailProper);
        }

        //---

        [Fact(DisplayName = "HasValidPassword Success")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithValidPassword_HasValidPassword_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithPassword("123");

            //Act


            //Assert
            registerNewUserCommand.HasValidPassword().Should().BeTrue();

            _notifications.Should().BeEmpty();
        }

        [Fact(DisplayName = "HasValidPassword Null")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithNullPassword_HasValidPassword_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithPassword(null);

            //Act


            //Assert
            registerNewUserCommand.HasValidPassword().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserRegisterPasswordRequired);
        }

        [Fact(DisplayName = "HasValidPassword Empty")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithEmptyPassword_HasValidPassword_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithPassword("");

            //Act


            //Assert
            registerNewUserCommand.HasValidPassword().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserRegisterPasswordProper);
        }

        [Fact(DisplayName = "HasUniqueUserEmail NonExistent")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithNonExistentEmail_HasUniqueUserEmail_MustReturnFalse()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithEmail("a@b.com.br");


            var userRepositoryMock = new Mock<IPersonRepository>();

            userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>()))
                .Returns(() => null);

            //Act


            //Assert
            registerNewUserCommand.HasUniqueUserEmail(userRepositoryMock.Object).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }


        [Fact(DisplayName = "HasUniqueUserEmail Existent")]
        [Trait("Category", "RegisterNewUserCommand Scopes")]
        public void
            RegisterNewUserCommand_WithExistentEmail_HasUniqueUserEmail_MustReturnFalse()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserRegisterCommand)new UserRegisterCommandBuilder()
                .WithEmail("a@b.com.br");


            var userRepositoryMock = new Mock<IPersonRepository>();

            userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>()))
                .Returns(() => new PersonBuilder()
                    .WithEmail("a@b.com.br"));

            //Act


            //Assert
            registerNewUserCommand.HasUniqueUserEmail(userRepositoryMock.Object).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserRegisterEmailAlreadyTaken);
        }


        [Fact(DisplayName = "HasUserName Success")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithNonNullUser_HasUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder()
                .WithUser("");

            //Act


            //Assert
            authenticateUserCommand.HasUserName().Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }


        [Fact(DisplayName = "HasUserName Null")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithNullUser_HasUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder()
                .WithUser(null);

            //Act


            //Assert
            authenticateUserCommand.HasUserName().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateUserRequired);
        }

        [Fact(DisplayName = "HasPassword Success")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithNonNullPassword_HasPassword_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder()
                .WithPassword("");

            //Act


            //Assert
            authenticateUserCommand.HasPassword().Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }


        [Fact(DisplayName = "HasPassword Null")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithNullPassword_HasPassword_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder()
                .WithPassword(null);

            //Act


            //Assert
            authenticateUserCommand.HasPassword().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticatePasswordRequired);
        }

        [Fact(DisplayName = "HasFoundAuthorizedUser Null Person")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithNullUser_HasFoundAuthorizedUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand) new UserAuthenticateCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasFoundAuthorizedUser(null).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateLoginFailed);
        }

        [Fact(DisplayName = "HasFoundAuthorizedUser Valid Person")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithValidUser_HasFoundAuthorizedUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasFoundAuthorizedUser(new PersonBuilder()).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }

        [Fact(DisplayName = "HasActiveUser Null")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithNullUser_HasActiveUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasActiveUser(null).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateUserIsInactive);
        }

        [Fact(DisplayName = "HasActiveUser Inactive Person")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithInactiveUser_HasActiveUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasActiveUser(new PersonBuilder().WithPersonUserStatus(PersonUserStatus.Inactive)).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateUserIsInactive);
        }

        [Fact(DisplayName = "HasActiveUser Active Person")]
        [Trait("Category", "AuthenticateUserCommand Scopes")]
        public void
            AuthenticateUserCommand_WithActiveUser_HasActiveUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserAuthenticateCommand)new UserAuthenticateCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasActiveUser(new PersonBuilder().WithPersonUserStatus(PersonUserStatus.Active)).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }




        [Fact(DisplayName = "HasValidEmail Success")]
        [Trait("Category", "UserForgotPasswordCommand Scopes")]
        public void
            UserForgotPasswordCommand_WithValidEmail_HasValidEmail_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserForgotPasswordCommand)new UserForgotPasswordCommandBuilder()
                .WithEmail("contato@viewt.com.br");

            //Act


            //Assert
            registerNewUserCommand.HasValidEmail().Should().BeTrue();

            _notifications.Should().BeEmpty();
        }

        [Fact(DisplayName = "HasValidEmail Null")]
        [Trait("Category", "UserForgotPasswordCommand Scopes")]
        public void
            UserForgotPasswordCommand_WithNullEmail_HasValidEmail_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserForgotPasswordCommand)new UserForgotPasswordCommandBuilder()
                .WithEmail(null);

            //Act


            //Assert
            registerNewUserCommand.HasValidEmail().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserForgotPasswordEmailRequired);
        }

        [Fact(DisplayName = "HasValidEmail Empty")]
        [Trait("Category", "UserForgotPasswordCommand Scopes")]
        public void
            UserForgotPasswordCommand_WithEmptyEmail_HasValidEmail_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var registerNewUserCommand = (UserForgotPasswordCommand)new UserForgotPasswordCommandBuilder()
                .WithEmail("");

            //Act


            //Assert
            registerNewUserCommand.HasValidEmail().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserForgotPasswordEmailProper);
        }

        [Fact(DisplayName = "HasSerialKey Success")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            RegisterNewUserCommand_WithValidSerialKey_HasSerialKey_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder()
                .WithSerialKey(Guid.NewGuid().ToString());

            //Act


            //Assert
            userChangePasswordCommand.HasSerialKey().Should().BeTrue();

            _notifications.Should().BeEmpty();
        }

        [Fact(DisplayName = "HasSerialKey Null")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            RegisterNewUserCommand_WithNullSerialKey_HasSerialKey_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder()
                .WithSerialKey(null);

            //Act


            //Assert
            userChangePasswordCommand.HasSerialKey().Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.SerialKeyRequired);
        }


        [Fact(DisplayName = "HasFoundPerson Null Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithNullUser_HasFoundUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            userChangePasswordCommand.HasFoundPerson(null).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserNotFound);
        }

        [Fact(DisplayName = "HasFoundPerson Valid Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithValidUser_HasFoundUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            userChangePasswordCommand.HasFoundPerson(new PersonBuilder()).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }


        [Fact(DisplayName = "HasFoundDevice Null Device")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithNullDevice_HasFoundDevice_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            userChangePasswordCommand.HasFoundDevice(null).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserChangePasswordDeviceNotFound);
        }

        [Fact(DisplayName = "HasFoundDevice Valid Device")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithValidDevice_HasFoundDevice_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            userChangePasswordCommand.HasFoundDevice(new DeviceBuilder()).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }


        [Fact(DisplayName = "HasFoundDeviceBelongsToPerson Device belongs to Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithDeviceBelongingToUser_HasFoundDeviceBelongsToUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            var person = new PersonBuilder()
                .WithPersonId(Guid.NewGuid());

            var device = new DeviceBuilder()
                .WithPerson(person);

            //Act


            //Assert
            userChangePasswordCommand.HasFoundDeviceBelongsToPerson(device, person).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }

        [Fact(DisplayName = "HasFoundDeviceBelongsToPerson Device does not belong to Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithDeviceNotBelongingToUser_HasFoundDeviceBelongsToUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var userChangePasswordCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            var person = new PersonBuilder()
                .WithPersonId(Guid.NewGuid());

            var device = new DeviceBuilder()
                .WithPerson(new PersonBuilder().WithPersonId(Guid.NewGuid()));

            //Act


            //Assert
            userChangePasswordCommand.HasFoundDeviceBelongsToPerson(device, person).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserChangePasswordDeviceDoesNotBelongToUser);
        }


        [Fact(DisplayName = "HasFoundAuthorizedUser Null Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithNullUser_HasFoundAuthorizedUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasFoundAuthorizedUser(null).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateLoginFailed);
        }

        [Fact(DisplayName = "HasFoundAuthorizedUser Valid Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithValidUser_HasFoundAuthorizedUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasFoundAuthorizedUser(new PersonBuilder()).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }

        [Fact(DisplayName = "HasActiveUser Null")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithNullUser_HasActiveUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasActiveUser(null).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateUserIsInactive);
        }

        [Fact(DisplayName = "HasActiveUser Inactive Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithInactiveUser_HasActiveUser_MustReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasActiveUser(new PersonBuilder().WithPersonUserStatus(PersonUserStatus.Inactive)).Should().BeFalse();

            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserAuthenticateUserIsInactive);
        }

        [Fact(DisplayName = "HasActiveUser Active Person")]
        [Trait("Category", "UserChangePasswordCommand Scopes")]
        public void
            UserChangePasswordCommand_WithActiveUser_HasActiveUser_MustReturnSuccess()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var authenticateUserCommand = (UserChangePasswordCommand)new UserChangePasswordCommandBuilder();

            //Act


            //Assert
            authenticateUserCommand.HasActiveUser(new PersonBuilder().WithPersonUserStatus(PersonUserStatus.Active)).Should().BeTrue();

            _notifications.Should()
                .BeEmpty();
        }




    }
}