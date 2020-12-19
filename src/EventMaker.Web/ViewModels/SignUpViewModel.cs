using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter Username")]
        [MaxLength(15, ErrorMessage = "Your username is too long")]
        [MinLength(4, ErrorMessage = "Your username is too small")]
        [Display(Name = nameof(Username))]
        public string Username { get; set; }

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
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        /// <summary>
        /// Password confirm.
        /// </summary>
        [Required(ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = nameof(ConfirmPassword))]
        public string ConfirmPassword { get; set; }
    }
}
