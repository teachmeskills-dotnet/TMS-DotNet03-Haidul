using System;
using EventMaker.Common.Interfaces;

namespace EventMaker.DAL.Entities
{
    /// <summary>
    /// Profile.
    /// </summary>
    public class Profile : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation to ApplicationUser.
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Email..
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// FirstName..
        /// </summary>
#nullable enable
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
