using EventMaker.BLL.Interfaces;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventMaker.BLL.Managers
{
    /// <inheritdoc cref="IAccountManager"/>
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileManager _profileManager;

        public AccountManager(UserManager<ApplicationUser> userManager,
                              IProfileManager profileManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        }

        public async Task<(IdentityResult, ApplicationUser)> SignUpAsync(string email, string userName, string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };

                var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _profileManager.CreateProfileAsync(email, userName, user.Id);
            }
            return (result, user);   /// Стоит ли тут сделать вызов метода с помощю делегата?
        }

        public async Task<string> GetUserIdByNameAsync(string name)
        {
            var user = await _userManager.Users.FirstAsync(u => u.UserName == name);
            if (user != null)
            {
                return user.Id;
            }
            throw new UserNotFoundException(ExceptionResource.UserNotFound);
        }

        public async Task<(IdentityResult, ApplicationUser)> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return (result, user);
            }
            throw new UserNotFoundException(ExceptionResource.UserNotFound);
        }


    }
}