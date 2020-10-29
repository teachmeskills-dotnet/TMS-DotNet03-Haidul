using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Sign in model.
    /// </summary>
    public class LogInViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [MaxLength(20)]
        [Display(Name = nameof(Username))]
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
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