using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Reset password view model.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required(ErrorMessage = "Please enter Email")]
        [MaxLength(40, ErrorMessage = "Your email is too long")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a email in format email@mail.com")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(8, ErrorMessage = "Your password is too short (Min lenght 8 symbols)")]
        [DataType(DataType.Password)]
        [Compare("PasswordConfirm", ErrorMessage = "Passwords do not match")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Code.
        /// </summary>
        public string Code { get; set; }
    }
}
