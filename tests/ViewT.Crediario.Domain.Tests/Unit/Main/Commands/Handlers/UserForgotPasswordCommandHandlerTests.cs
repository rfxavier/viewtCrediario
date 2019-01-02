using AutoMoq;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using ViewT.Crediario.Domain.Core.Commands;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Core.Interfaces;
using ViewT.Crediario.Domain.Main.Commands.Handlers;
using ViewT.Crediario.Domain.Main.Commands.Results;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Events;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Commands.Builders;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;
using Xunit;

namespace ViewT.Condominio.Domain.Tests.Unit.Main.Commands.Handlers
{
    public class UserForgotPasswordCommandHandlerTests
    {
        public readonly AutoMoqer _mocker;

        private readonly IList<DomainNotification> _notifications = new List<DomainNotification>();
        private UserForgotPasswordRequestedEvent _eventUserForgotPasswordRequested;
        private bool _eventUserForgotPasswordRequestedFired = false;

        public UserForgotPasswordCommandHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "UserForgotPasswordCommandHandler Handle invalid not from repository")]
        [Trait("Category", "UserForgotPasswordCommandHandler")]
        public void
            UserForgotPasswordCommand_WithInvalidPropertiesNotFromRepositories_Handle_ShouldReturnAllErrors()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var invalidCommand = new UserForgotPasswordCommandBuilder()
                .WithEmail("abc");

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            handler.Handle(invalidCommand);

            //Assert
            _notifications.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.Contain(n => n.Value == Domain.Main.Resources.Messages.UserForgotPasswordEmailProper);

            _mocker.GetMock<IPersonRepository>().Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Never());
        }

        [Fact(DisplayName = "UserForgotPasswordCommandHandler Handle valid-user not found")]
        [Trait("Category", "UserForgotPasswordCommandHandler")]
        public void
            UserForgotPasswordCommand_ValidAndNotFoundUser_Handle_ShouldDoNothingAndReturnEmptyResponse()
        {
            //Arrange
            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var validCommand = new UserForgotPasswordCommandBuilder()
                .WithEmail("login@domain.com");

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetByEmail(It.IsAny<string>()))
                .Returns(() => null);

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            var commandResult = handler.Handle(validCommand);

            //Assert
            _notifications.Should()
                .BeEmpty();

            _mocker.GetMock<IPersonRepository>().Verify(x => x.Update(It.IsAny<Person>()), Times.Never());

            commandResult.Should()
                .BeEquivalentTo(new UserForgotPasswordCommandResult()
                {
                    SerialKey = string.Empty
                });
        }

        //[Fact(DisplayName = "UserForgotPasswordCommandHandler Handle valid-user found")]
        //[Trait("Category", "UserForgotPasswordCommandHandler")]
        //public void
        //    UserForgotPasswordCommand_ValidAndFoundUser_Handle_ShouldResetPasswordAndRaiseEvent()
        //{
        //    //Arrange
        //    UserForgotPasswordRequestedEventHandlerForEmailNotification handlerEmailNotification = null;

        //    DomainEvent.Register<UserForgotPasswordRequestedEvent>(ev => {
        //        _eventUserForgotPasswordRequested = ev;
        //        _eventUserForgotPasswordRequestedFired = true;
        //        handlerEmailNotification.Handle(_eventUserForgotPasswordRequested);
        //    });

        //    DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

        //    var validCommand = new UserForgotPasswordCommandBuilder()
        //        .WithEmail("login@domain.com");

        //    var currentUserPassword = "userPassword";

        //    Person user = new PersonBuilder()
        //        .WithPassword(currentUserPassword);

        //    _mocker.GetMock<IUserRepository>()
        //        .Setup(u => u.GetByEmail(It.IsAny<string>()))
        //        .Returns(() => user);

        //    _mocker.GetMock<IUserRepository>()
        //        .Setup(u => u.Update(It.IsAny<Person>()))
        //        .Returns(() => user);

        //    _mocker.GetMock<IUnitOfWork>()
        //        .Setup(u => u.Commit())
        //        .Returns(CommandResponse.Ok);

        //    var handler = _mocker.Resolve<UserCommandHandler>();

        //    handlerEmailNotification = _mocker.Resolve<UserForgotPasswordRequestedEventHandlerForEmailNotification>();

        //    //Act
        //    handler.Handle(validCommand);

        //    //Assert
        //    _notifications.Should()
        //        .BeEmpty();

        //    user.Password.Should()
        //        .NotBe(currentUserPassword);

        //    _mocker.GetMock<IUserRepository>().Verify(x => x.Update(It.IsAny<Person>()),Times.Once());
        //    _mocker.GetMock<IUnitOfWork>().Verify(uow => uow.Commit(),Times.Once());
        //    _eventUserForgotPasswordRequestedFired.Should().BeTrue();
        //    //_mocker.GetMock<IHandler<UserForgotPasswordRequestedEvent>>().Verify(h => h.Handle(It.IsAny<UserForgotPasswordRequestedEvent>()),Times.Once());
        //    //_mocker.GetMock<IEmailNotificationRepository>().Verify(n => n.Add(It.IsAny<EmailNotification>()), Times.Once());
        //}

        [Fact(DisplayName = "UserForgotPasswordCommandHandler Handle valid-user found")]
        [Trait("Category", "UserForgotPasswordCommandHandler")]
        public void
            UserForgotPasswordCommand_ValidAndFoundUser_Handle_ShouldResetPasswordRaiseEventAndReturnProperResponse()
        {
            //Arrange
            DomainEvent.Register<UserForgotPasswordRequestedEvent>(ev => {
                _eventUserForgotPasswordRequestedFired = true;
            });

            DomainEvent.Register<DomainNotification>(dn => _notifications.Add(dn));

            var validCommand = new UserForgotPasswordCommandBuilder()
                .WithEmail("login@domain.com");

            var currentUserPassword = "userPassword";

            Person person = new PersonBuilder()
                .WithPassword(currentUserPassword);

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.GetByEmail(It.IsAny<string>()))
                .Returns(() => person);

            _mocker.GetMock<IPersonRepository>()
                .Setup(u => u.Update(It.IsAny<Person>()))
                .Returns(() => person);

            _mocker.GetMock<IUnitOfWork>()
                .Setup(u => u.Commit())
                .Returns(CommandResponse.Ok);

            var handler = _mocker.Resolve<UserCommandHandler>();

            //Act
            var commandResult = handler.Handle(validCommand);

            //Assert
            _notifications.Should()
                .BeEmpty();

            person.Password.Should()
                .NotBe(currentUserPassword);

            _mocker.GetMock<IPersonRepository>().Verify(x => x.Update(It.IsAny<Person>()), Times.Once());
            _mocker.GetMock<IUnitOfWork>().Verify(uow => uow.Commit(), Times.Once());
            _eventUserForgotPasswordRequestedFired.Should().BeTrue();

            commandResult.Should()
                .BeEquivalentTo(new UserForgotPasswordCommandResult()
                {
                    SerialKey = person.SerialKey
                });
        }

    }
}