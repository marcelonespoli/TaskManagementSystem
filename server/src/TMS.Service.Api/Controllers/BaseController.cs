using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TMS.Domain.Core.Notifications;
using TMS.Domain.Interfaces;

namespace TMS.Service.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public BaseController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NofifyErrorModelInvalid()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, errorMsg);
            }
        }

        protected void NotifyError(string codigo, string errorMsg)
        {
            _mediator.PublishEvent(new DomainNotification(codigo, errorMsg));
        }


        protected bool IsModelStateValid()
        {
            if (ModelState.IsValid) return true;

            NofifyErrorModelInvalid();
            return false;
        }
    }
}
