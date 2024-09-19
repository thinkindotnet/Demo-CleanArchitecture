using Microsoft.AspNetCore.Authorization;

using MyApp.Application.Common.Interfaces;
using MyApp.Application.Common.Models;
using MyApp.Infrastructure.Extensions;


namespace MyApp.Infrastructure.Services;


public class IdentityService
    : IIdentityService
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<IdentityUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;


    public IdentityService(
        UserManager<IdentityUser> userManager,
        IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }


    #region IIdentityService members

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName ?? string.Empty;
    }


    public async Task<(Result result, string userId)> CreateUserAsync(
        string userName,
        string password)
    {
        var user = new IdentityUser
        {
            UserName = userName,
            Email = userName
        };

        var result = await _userManager.CreateAsync(user, password);
        return (result.ToApplicationResult(), user.Id);
    }


    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }

        return Result.Success();
    }


    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user is not null && await _userManager.IsInRoleAsync(user, role);
    }


    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    #endregion

}
