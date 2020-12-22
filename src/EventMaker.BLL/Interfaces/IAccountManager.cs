using System.Threading.Tasks;
using EventMaker.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventMaker.BLL.Interfaces
{
    /// <summary>
    /// Account manager.
    /// </summary>
    public interface IAccountManager
    {
        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="username">Username.</param>
        /// <returns>Task<(IdentityResult, ApplicationUser, string code)>.</returns>
        Task<(IdentityResult, ApplicationUser, string code)> SignUpAsync(string email, string username, string password);

        /// <summary>
        /// Confirm email.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="code">Code.</param>
        /// <returns>Task<IdentityResult></returns>
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);

        /// <summary>
        /// Get user id by name.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns>Identifier (GUID).</returns>
        Task<string> GetUserIdByNameAsync(string name);

        /// <summary>
        /// Get user name by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Task<string></returns>
        Task<string> GetUserNameByIdAsync(string id);

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="code">Code.</param>
        /// <param name="password">Password.</param>
        /// <returns>Task<IdentityResult></returns>
        Task<IdentityResult> ResetPasswordAsync(string email, string code, string password);

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>(IdentityResult , ApplicationUser).</returns>
        Task<(IdentityResult, ApplicationUser)> ChangePasswordAsync(string id, string oldPassword, string newPassword);

        /// <summary>
        /// Restore password by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Task<(ApplicationUser, string, bool)></returns>
        Task<(ApplicationUser, string, bool)> ForgotPasswordAsync(string email);
    }
}
