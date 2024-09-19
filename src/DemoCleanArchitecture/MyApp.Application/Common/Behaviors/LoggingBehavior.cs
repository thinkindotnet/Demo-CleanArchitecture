using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

using MyApp.Application.Common.Interfaces;


namespace MyApp.Application.Common.Behaviors;


/// <summary>
///     Custom Logging Behavior middleware.
/// </summary>
/// <typeparam name="TRequest">Request pipeline object.</typeparam>
public class LoggingBehavior<TRequest>
    : IRequestPreProcessor<TRequest> where TRequest : class
{

    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public LoggingBehavior(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }


    #region IRequestPreProcessor members

    public async Task Process(
        TRequest request,
        CancellationToken cancellationToken)
    {

        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        var userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}", 
            requestName, userId, userName, request);
    }

    #endregion
}
