using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Change password view model.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Old password.
        /// </summary>
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(8, ErrorMessage = "Your password is too short (Min lenght 8 symbols)")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(OldPassword))]
        public string OldPassword { get; set; }

        /// <summary>
        /// New password.
        /// </summary>
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(8, ErrorMessage = "Your password is too short (Min lenght 8 symbols)")]
        [DataType(DataType.Password)]
        [Compare("NewPasswordConfirm", ErrorMessage = "Passwords do not match")]
        [Display(Name = nameof(NewPassword))]
        public string NewPassword { get; set; }

        /// <summary>
        /// Old password.
        /// </summary>
        [Required(ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = nameof(NewPassword))]
        public string NewPasswordConfirm { get; set; }
    }
}
