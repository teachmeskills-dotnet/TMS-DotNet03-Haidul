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
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Events)
             .HasKey(events => events.Id);

            builder.Property(events => events.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(events => events.AuthorName)
               .IsRequired()
               .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(events => events.Title)
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(events => events.Info)
                .IsRequired()
                .HasMaxLength(ConfigurationContants.SqlMaxLengthLong);

            builder.Property(events => events.Format)
                .IsRequired();

            builder.Property(events => events.Created)
                .HasColumnType(ConfigurationContants.SqlDateFormat);

            builder.Property(events => events.Started)
                .IsRequired()
                .HasColumnType(ConfigurationContants.SqlDateFormat);

            builder.Property(events => events.Created)
                .HasColumnType(ConfigurationContants.SqlDateFormat);

            builder.Property(events => events.PNumber)
                .IsRequired();

            builder.HasOne(events => events.User)
                .WithMany(user => user.Events)
                .HasForeignKey(events => events.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
