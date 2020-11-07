using System;
using System.Threading.Tasks;
using EventMaker.BLL.Interfaces;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<(IdentityResult, ApplicationUser, string code)> SignUpAsync(string email, string userName, string password)
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
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return (result, user, code);
            }
            throw new OtherException(ExceptionResource.NotCreated);
        }

        public async Task<string> GetUserIdByNameAsync(string name)
        {
            var user = await _userManager.Users.FirstAsync(u => u.UserName == name);
            if (user != null)
            {
                return user.Id;
            }
            throw new NotFoundException(ExceptionResource.UserNotFound);
        }

        public async Task<string> GetUserNameByIdAsync(string id)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            if (user != null)
            {
                return user.UserName;
            }
            throw new NotFoundException(ExceptionResource.UserNotFound);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string code, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, code, password);
                return result;
            }
            throw new NotFoundException(ExceptionResource.UserNotFound);
        }

        public async Task<(IdentityResult, ApplicationUser)> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return (result, user);
            }
            throw new NotFoundException(ExceptionResource.UserNotFound);
        }

        public async Task<(ApplicationUser, string, bool)> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return (null, "", false);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return (user, code, true);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    return result;
                }
            }
            throw new NotFoundException(ExceptionResource.UserNotFound);
        }
    }
}
