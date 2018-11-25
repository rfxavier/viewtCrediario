using System.Linq;
using System.Net;
using System.Web.Http;
using ViewT.Crediario.Domain.Core.DomainNotification.Events;
using ViewT.Crediario.Domain.Core.Interfaces;

namespace ViewT.Crediario.Services.REST.Api.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _notifications = DomainEvent.Container.GetService<IDomainNotificationHandler<DomainNotification>>();
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult Response(object result)
        {
            if (!_notifications.HasNotifications())
            {
                try
                {
                    _unitOfWork.Commit();
                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }
                catch
                {
                    // Logar o erro (Elmah)
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        success = false,
                        errors = new[] { "Ocorreu uma falha interna no servidor." }
                    });
                }
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, new
                {
                    success = false,
                    errors = _notifications.Notify().Select(n => n.Value)
                });
            }
        }
    }
}