using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Sign in model.
    /// </summary>
    public class LogInViewModel
    {
        /// <summary>
        /// Username.
        /// </summary>
        [Required(ErrorMessage = "Please enter Username")]
        [MaxLength(15, ErrorMessage = "Your username is too long")]
        [MinLength(4, ErrorMessage = "Your username is too small")]
        [Display(Name = nameof(Username))]
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(8, ErrorMessage = "Your password is too short (Min lenght 8 symbols)")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        /// <summary>
        /// Remember me.
        /// </summary>
        [Display(Name = nameof(RememberMe))]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Return Url.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
