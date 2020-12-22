using System;
using EventMaker.Common.Constants;
using EventMaker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventMaker.DAL.Configurations
{
    /// <summary>
    /// EF configuration for Profile entity
    /// </summary>
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.EventParticipants)
             .HasKey(eventparticipants => eventparticipants.Id);

            builder.HasOne(eventparticipant => eventparticipant.User)
               .WithMany(user => user.EventParticipants)
               .HasForeignKey(eventparticipant => eventparticipant.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(eventparticipant => eventparticipant.Event)
             .WithMany(events => events.EventParticipants)
             .HasForeignKey(eventparticipant => eventparticipant.EventId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
