using System;

namespace EventMaker.BLL.Models
{
    /// <summary>
    /// Profile data transfer object.
    /// </summary>
    public class ProfileDto
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        /// <summary>
        /// Email..
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        public string Username { get; set; }

        ///<summary>
        /// Avatar.
        ///</summary>
        public byte[] Image { get; set; }

#nullable enable
        /// <summary>
        /// FirstName..
        /// </summary>

        public string? FirstName { get; set; }

        /// <summary>
        /// LastName..
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Age..
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Birth date.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Date when account was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Telephone number.
        /// </summary>
        /// 
        public string? Phone { get; set; }

        /// <summary>
        /// Telegram username.
        /// </summary>

        public string? Telegram { get; set; }

        /// <summary>
        /// Social network.
        /// </summary>
        public string? SocialNetwork { get; set; }
    }
}
