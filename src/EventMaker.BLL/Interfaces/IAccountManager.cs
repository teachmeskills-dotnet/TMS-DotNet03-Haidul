using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <returns>IdentityResult</returns>
        Task<IdentityResult> RegisterAsync(string email , string password , string username);
    }
}
 