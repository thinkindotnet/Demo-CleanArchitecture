using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using MyApp.Application.Common.Exceptions;

namespace MyApp.WebMvcUI.Filters;

public class ApiExceptionFilterAttribute
    : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;


    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers 
            = new Dictionary<Type, Action<ExceptionContext>> 
              {
                { typeof(MyValidationException), HandleValidationException },
                { typeof(MyNotFoundException), HandleValidationException }
              };
    }

    private void HandleValidationException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }


    #region Helper methods

    private void HandleException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    #endregion
}
