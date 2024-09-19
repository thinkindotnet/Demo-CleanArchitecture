namespace MyApp.Application.Common.Interfaces;

public interface IIdentityService
{

    Task<string> GetUserNameAsync(string userId);


    Task<(Result result, string userId)> CreateUserAsync(string userName, string password);


    Task<Result> DeleteUserAsync(string userId);


    Task<bool> IsInRoleAsync(string userId, string role);


    Task<bool> AuthorizeAsync(string userId, string policyName);
}
