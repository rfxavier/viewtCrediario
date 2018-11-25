using System;
using System.Collections.Generic;
using AutoMoq;
using FluentAssertions;
using Moq;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Main.Commands.Handlers;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;
using Xunit;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Handlers
{
    public class UserRegisterCommandHandlerTests
    {
        private readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();

        public UserRegisterCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "UserRegisterCommandHandler Handle invalid not from repository")]
        [Trait("Category", "UserRegisterCommandHandler")]
        public void
            UserRegisterCommand_WithInvalidPropertiesNotFromRepositories_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var invalidCommand = new UserRegisterCommandBuilder()
                .WithEmail("abc")
                .WithPassword("");

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(invalidCommand);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.EmailProper)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserRegisterPasswordProper);

            _mocker.GetMock<IPersonRepository>().Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Never());

        }


        [Fact(DisplayName = "UserRegisterCommandHandler Handle invalid from repository")]
        [Trait("Category", "UserRegisterCommandHandler")]
        public void
            UserRegisterCommand_WithInvalidPropertiesFromRepositories_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var existingUserEmail = "user@email.com";

            var invalidCommand = new UserRegisterCommandBuilder()
                .WithPassword("123")
                .WithEmail(existingUserEmail);

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetByEmail(It.IsAny<string>()))
                .Returns(() => new PersonBuilder()
                    .WithEmail(existingUserEmail));

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(invalidCommand);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserRegisterEmailAlreadyTaken);

            _mocker.GetMock<IPersonRepository>().Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once());
        }

        [Fact(DisplayName = "UserRegisterCommandHandler Handle valid")]
        [Trait("Category", "UserRegisterCommandHandler")]
        public void UserRegisterCommand_Valid_Handle_ShouldReturnSuccessAndGeneratedUserId()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            Person person = null;

            var validCommand = new UserRegisterCommandBuilder()
                .WithPassword("12345")
                .WithEmail("abc@def.com");

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetByEmail(It.IsAny<string>()))
                .Returns(() => null);

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.Add(It.IsAny<Person>()))
                .Callback((Person u) =>
                {
                    person = new PersonBuilder()
                        .WithPersonId(u.PersonId);
                })
                .Returns(() => person);

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(validCommand);

            //Assert
            _notifications.Should().BeEmpty();
            person.Should().NotBeNull();
            person.PersonId.Should().NotBe(Guid.Empty);
        }
    }
}