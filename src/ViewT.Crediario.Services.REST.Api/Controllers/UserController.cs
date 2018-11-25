using System.Web.Http;
using ViewT.Crediario.Domain.Core.Commands;
using ViewT.Crediario.Domain.Core.Interfaces;
using ViewT.Crediario.Domain.Main.Commands.Inputs;
using ViewT.Crediario.Domain.Main.Commands.Results;

namespace ViewT.Crediario.Services.REST.Api.Controllers
{
    [RoutePrefix("User")]
    public class UserController : BaseController
    {
        private readonly ICommandHandler<UserRegisterCommand> _handlerRegister;
        private readonly ICommandHandler<UserAuthenticateCommand> _handlerAuthenticate;
        private readonly ICommandHandler<UserChangePasswordCommand> _handlerChangePassword;
        private readonly ICommandHandler<UserForgotPasswordCommand> _handlerForgotPassword;

        public UserController(
            ICommandHandler<UserRegisterCommand> handlerRegister,
            ICommandHandler<UserAuthenticateCommand> handlerAuthenticate,
            ICommandHandler<UserChangePasswordCommand> handlerChangePassword,
            ICommandHandler<UserForgotPasswordCommand> handlerForgotPassword,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _handlerRegister = handlerRegister;
            _handlerAuthenticate = handlerAuthenticate;
            _handlerChangePassword = handlerChangePassword;
            _handlerForgotPassword = handlerForgotPassword;
        }

        [HttpPost]
        [Route("Register/v1")]
        public IHttpActionResult Register(UserRegisterCommand command)
        {
            UserRegisterCommandResult result = (UserRegisterCommandResult)_handlerRegister.Handle(command);
            return Response(result);
        }

        [HttpPost]
        [Route("Authenticate/v1")]
        public IHttpActionResult AuthenticateUser(UserAuthenticateCommand command)
        {
            UserAuthenticateCommandResult result = (UserAuthenticateCommandResult)_handlerAuthenticate.Handle(command);
            return Response(result);
        }

        [HttpPost]
        [Route("ChangePassword/v1")]
        public IHttpActionResult UserChangePassword(UserChangePasswordCommand command)
        {
            UserChangePasswordCommandResult result = (UserChangePasswordCommandResult)_handlerChangePassword.Handle(command);
            return Response(result);
        }

        [HttpPost]
        [Route("ForgotPassword/v1")]
        public IHttpActionResult UserForgotPassword(UserForgotPasswordCommand command)
        {
            UserForgotPasswordCommandResult result = (UserForgotPasswordCommandResult)_handlerForgotPassword.Handle(command);
            return Response(result);
        }

    }
}