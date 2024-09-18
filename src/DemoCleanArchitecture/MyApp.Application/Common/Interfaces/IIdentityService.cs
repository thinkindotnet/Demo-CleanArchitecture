namespace MyApp.Application.Common.Interfaces;

public interface IIdentityService
{

    Task<string> GetUserNameAsync(string userId);


    Task<(Result result, string userId)> CreateUserAsync(string userName, string password);


    Task<Result> DeleteUserAsync(string userId);

}
