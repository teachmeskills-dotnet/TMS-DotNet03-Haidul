using EventMaker.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventMaker.Web.ViewModels
{
    /// <summary>
    /// Event view model.
    /// </summary>
    public class EventViewModel
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
        /// Author.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Title
        /// </summary>
#nullable enable

        [Required]
        [Display(Name = nameof(Title))]
        public string? Title { get; set; }

        /// <summary>
        /// Info.
        /// </summary>
#nullable enable

        [Required]
        [Display(Name = nameof(Info))]
        public string? Info { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
#nullable enable
        public string? Comment { get; set; }

        /// <summary>
        /// Day of creation
        /// </summary>
        [Required]
        [Display(Name = nameof(Created))]
        public DateTime Created { get; set; }

        /// <summary>
        /// Start of the event.
        /// </summary>
        [Required]
        [Display(Name = nameof(Started))]
        public DateTime Started { get; set; }

        /// <summary>
        /// End of the event.
        /// </summary>
        public DateTime? Closed { get; set; }

        /// <summary>
        /// Format.
        /// </summary>
        [Required]
        [Display(Name = nameof(Format))]
        public EventFormats? Format { get; set; }

        /// <summary>
        /// Participants number
        /// </summary>
        [Required]
        [Display(Name = nameof(PNumber))]
        public int? PNumber { get; set; }

        /// <summary>
        /// Remaining free seats of participants.
        /// </summary>
        public int? PFreeNumber { get; set; }

        public IEnumerable<string>? EventParticipants { get; set; }
    }
}
