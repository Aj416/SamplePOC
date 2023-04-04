using Core.Service.Bus;
using Core.Service.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Service.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly IDomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public ApiController(IDomainNotificationHandler notifications, IMediatorHandler mediator)
        {
            _notifications = notifications;
            _mediator = mediator;
        }

        private bool IsValidOperation() => !_notifications.HasNotifications();

        protected void NotifyError(string key, string message, int code = 0) => _mediator.RaiseEvent(new DomainNotification(key, message, code));

        protected void NotifyModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, errorMsg);
            }
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return result != null ? Ok(result) : (IActionResult)NoContent();
            }
            else
            {
                var customCodeStatusResponse = _notifications.GetNotifications()
                    .Where(x => x.Code != 0)
                    .FirstOrDefault();

                var allNotifications = _notifications.GetNotifications().Select(x => x.Value).Aggregate((a, b) => a + ", " + b);

                var problemDetails = new ProblemDetails()
                {
                    Status = customCodeStatusResponse?.Code ?? (int)HttpStatusCode.BadRequest,
                    Title = "InValid Request",
                    Detail = customCodeStatusResponse?.Value ?? allNotifications
                };

                return StatusCode(problemDetails.Status.Value, problemDetails);
            }
        }

        protected new IActionResult Response(int statusCode, object result = null)
        {
            if (IsValidOperation())
            {
                return StatusCode(statusCode, result);
            }
            else
            {
                return Response(result);
            }
        }
    }
}
