using System.Diagnostics;

using Microsoft.Extensions.Logging;

using MyApp.Application.Common.Interfaces;


namespace MyApp.Application.Common.Behaviors;


/// <summary>
///     Custom Performance Behavior middleware to log long running requests.
/// </summary>
/// <typeparam name="TRequest">Request pipeline object.</typeparam>
/// <typeparam name="TResponse">Response pipeline object.</typeparam>
class PerformanceBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public PerformanceBehavior(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    #region IPipelineBehaviour members

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }

            _logger.LogWarning("Long Running request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;

    }

    #endregion
}
