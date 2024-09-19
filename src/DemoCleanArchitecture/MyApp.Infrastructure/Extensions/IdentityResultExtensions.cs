using MyApp.Application.Common.Models;


namespace MyApp.Infrastructure.Extensions;


public static class IdentityResultExtensions
{

    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
               ? Result.Success()
               : Result.Failure(result.Errors.Select(e => e.Description));
    }

}
