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
        [Required]
        [Display(Name = nameof(OldPassword))]
        public string OldPassword { get; set; }

        /// <summary>
        /// New password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword))]
        public string NewPassword { get; set; }

        /// <summary>
        /// Old password.
        /// </summary>
        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword))]
        public string NewPasswordConfirm { get; set; }
    }
}