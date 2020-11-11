using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Profile.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }

        /// <summary>
        /// Username.
        [Required]
        [MaxLength(15)]
        public string Username { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public IFormFile Image { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] ByteImage { get; set; }

        /// <summary>
        /// FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// BirthDate.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        public int Phone { get; set; }

        /// <summary>
        /// Telegram.
        /// </summary>
        public string Telegram { get; set; }

        /// <summary>
        /// SocialNetwork.
        /// </summary>
        public string SocialNetwork { get; set; }
    }
}
