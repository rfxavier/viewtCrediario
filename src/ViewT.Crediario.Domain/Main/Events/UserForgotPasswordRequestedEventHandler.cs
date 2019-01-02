namespace ViewT.Crediario.Domain.Main.Events
{
    public class UserForgotPasswordRequestedEventHandler : IHandler<UserForgotPasswordRequestedEvent>
    {
        private readonly IEmailService _emailService;

        public UserForgotPasswordRequestedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public void Handle(UserForgotPasswordRequestedEvent args)
        {
            _emailService.SendEmailForgotPassword(args.Person.Email, args.PlainNewPassword);
        }
    }
}
