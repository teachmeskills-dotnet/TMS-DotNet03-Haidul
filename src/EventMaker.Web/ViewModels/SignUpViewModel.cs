using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Sign up model.
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// UserName.
        /// </summary>
        [Required]
        [MaxLength(15)]
        [Display(Name = nameof(Username))]
        public string Username { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [MaxLength(20)]
        [DataType(DataType.EmailAddress , ErrorMessage = "Please enter a email in format email@mail.com")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordConfirm", ErrorMessage = "Passwords do not match")]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        /// <summary>
        /// Password confirm.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = nameof(PasswordConfirm))]
        public string PasswordConfirm { get; set; }
    }
}
