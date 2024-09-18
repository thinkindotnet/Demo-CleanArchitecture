using Microsoft.AspNetCore.Identity;

using MyApp.Application.Common.Interfaces;
using MyApp.Application.Common.Models;

namespace MyApp.Infrastructure.Identity
{
    public class IdentityService
        : IIdentityService
    {

        public readonly UserManager<IdentityUser> _userManager;


        public IdentityService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }



        #region IIdentityService members

        public async Task<string> GetUserNameAsync(string userId)
        {
            // var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
            var user = await _userManager.FindByIdAsync(userId);

            return user?.UserName ?? string.Empty;
        }


        public async Task<(Result result, string userId)> CreateUserAsync(
            string userName, string password)
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

        #endregion

    }
}
