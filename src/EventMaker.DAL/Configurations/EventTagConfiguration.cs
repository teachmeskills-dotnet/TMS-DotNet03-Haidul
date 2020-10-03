using EventMaker.Common.Constants;
using EventMaker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EventMaker.DAL.Configurations
{
    /// <summary>
    /// EF configuration for EventTag entity
    /// </summary>
    public class EventTagConfiguration : IEntityTypeConfiguration<EventTag>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<EventTag> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.EventTags)
             .HasKey(eventparticipants => eventparticipants.Id);

            builder.HasOne(eventtag => eventtag.Tag)
               .WithMany(tag => tag.EventTags)
               .HasForeignKey(eventparticipant => eventparticipant.TagId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(eventtag => eventtag.Event)
             .WithMany(events => events.EventTags)
             .HasForeignKey(eventparticipant => eventparticipant.EventId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
