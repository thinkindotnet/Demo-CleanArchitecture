using System.Security.Claims;

using MyApp.Application.Common.Interfaces;

namespace MyApp.WebMvcUI.Services.CurrentUser;

public class CurrentUserService
    : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }


    #region ICurrentUserService members

    public string? UserId => 
        _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    #endregion

}
