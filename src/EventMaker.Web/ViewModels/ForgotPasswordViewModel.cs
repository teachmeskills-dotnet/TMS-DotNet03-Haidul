using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Forgot password view model.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required(ErrorMessage = "Please enter Email")]
        [MaxLength(40, ErrorMessage = "Your email is too long")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a email in format email@mail.com")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }
    }
}
