using Demo.Business.Contracts;
using Demo.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Net.Mime;

namespace Demo.API.Controllers
{
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
            => _notifier = notifier;

        protected virtual bool IsValidOperation()
            => !_notifier.HasNotification();

        protected virtual ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
                return Ok(new { 
                
                    success = true,
                    data = result

                });

            return BadRequest(new
            {

                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)

            });
        }

        protected virtual ActionResult CustomResponse(ModelStateDictionary modelState) 
        {
            if (!modelState.IsValid)
                NotifyErrorInvalidModelState(modelState);

            return CustomResponse();
        }

        protected virtual void NotifyErrorInvalidModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected virtual void NotifyError(string errorMessage)
            => _notifier.Handle(new Notification(errorMessage));

        protected virtual void Notify(string message)
            => _notifier.Handle(new Notification(message));
    }
}
