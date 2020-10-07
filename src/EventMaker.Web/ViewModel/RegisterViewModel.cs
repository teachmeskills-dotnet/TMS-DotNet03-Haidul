using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Web.ViewModel
{
    /// <summary>
    /// Register model.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// UserName.
        /// </summary>
        [Required]
        [Display(Name = nameof(Username))]
        public string Username { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        /// <summary>
        /// Password confirm.
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(PasswordConfirm))]
        public string PasswordConfirm { get; set; }
    }
}
