using System;

namespace EventMaker.Web.ViewModels
{
    public class ProfileEditViewModel
    {
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

