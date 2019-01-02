using AutoMoq;
using FluentAssertions;
using Moq;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Events;
using ViewT.Crediario.Domain.Main.Interfaces;
using ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders;
using Xunit;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Commands.EventHandlers
{
    public class UserForgotPasswordRequestedEventHandlerTests
    {
        public readonly AutoMoqer _mocker;

        private UserForgotPasswordRequestedEvent _eventUserForgotPasswordRequested;

        private const string ValidUserName = "login@domain.com";
        private const string ValidPassword = "1234";

        public UserForgotPasswordRequestedEventHandlerTests()
        {
            DomainEvent.ClearCallbacks();

            _mocker = new AutoMoqer();
        }

        [Fact(DisplayName = "UserForgotPasswordEventHandler HandlerForEmailNotification")]
        [Trait("Category", "UserForgotPasswordEventHandler")]
        public void
            UserForgotPasswordEventHandler_HandlerForEmailNotificationHandle_ShouldAddProperEmailNotification()
        {
            //Arrange
            UserForgotPasswordRequestedEventHandler handlerEmailNotification = null;

            DomainEvent.Register<UserForgotPasswordRequestedEvent>(ev =>
            {
                _eventUserForgotPasswordRequested = ev;
                handlerEmailNotification.Handle(_eventUserForgotPasswordRequested);
            });

            Person person = new PersonBuilder()
                .WithEmail(ValidUserName)
                .WithPassword(ValidPassword);

            EmailNotification emailNotification = null;

            _mocker.GetMock<IEmailNotificationRepository>()
                .Setup(n => n.Add(It.IsAny<EmailNotification>()))
                .Callback((EmailNotification e) =>
                {
                    emailNotification = new EmailNotificationBuilder()
                        .WithEmailTo(e.EmailTo)
                        .WithBodyText(e.BodyText);
                })
                .Returns(() => emailNotification);

            handlerEmailNotification = _mocker.Resolve<UserForgotPasswordRequestedEventHandler>();

            //Act
            handlerEmailNotification.Handle(new UserForgotPasswordRequestedEvent(person));

            //Assert
            emailNotification.Should()
                .NotBeNull();
            emailNotification.EmailTo.Should()
                .Be(person.Email);
            emailNotification.BodyText.Should()
                .Contain(person.Password);

            _mocker.GetMock<IEmailNotificationRepository>().Verify(x => x.Add(It.IsAny<EmailNotification>()), Times.Once());
        }
    }
}