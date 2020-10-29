using EventMaker.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
        /// <returns>IdentityResult.</returns>
        Task<(IdentityResult, ApplicationUser)> SignUpAsync(string email, string username, string password);

        /// <summary>
        /// Get user id by name.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns>Identifier (GUID).</returns>
        Task<string> GetUserIdByNameAsync(string name);

        /// <summary>
        /// Change password async
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>(IdentityResult , ApplicationUser).</returns>
        /// <returns>Null if user not found.</returns>
        Task<(IdentityResult, ApplicationUser)> ChangePasswordAsync(string id, string oldPassword, string newPassword);
    }
}