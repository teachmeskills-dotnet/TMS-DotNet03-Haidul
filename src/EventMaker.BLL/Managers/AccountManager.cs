using EventMaker.BLL.Interfaces;
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

        public AccountManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        }

        public async Task<(IdentityResult, ApplicationUser)> SignUpAsync(string email, string username, string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);
            return (result, user);
        }

        public async Task<string> GetUserIdByNameAsync(string name)
        {
            var user = await _userManager.Users.FirstAsync(u => u.UserName == name);
            return user.Id;
        }
        
        public async Task<(IdentityResult , ApplicationUser)> ChangePasswordAsync(string id , string oldPassword , string newPassword)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return (result, user);
            }
            return (null, null);
        }
    }
}
