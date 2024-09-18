
using MediatR;

using Microsoft.Extensions.Logging;

namespace MyApp.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
{

    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(
        ILogger<TRequest> logger)
    {
        _logger = logger;
    }


    #region IPipelineBehavior Members

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(
                exception: ex, 
                message: "Unhandled Exception for Request {Name} {@Request}",
                requestName, 
                request);

            throw;
        }
    }

    #endregion
}
