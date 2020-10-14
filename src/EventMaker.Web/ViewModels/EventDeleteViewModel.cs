using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Delete event view model.
    /// </summary>
    public class EventDeleteViewModel
    {
        /// <summary>
        /// Event id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Title
        /// </summary>
#nullable enable
        [Display(Name = nameof(Title))]
        public string? Title { get; set; }

    }
}
