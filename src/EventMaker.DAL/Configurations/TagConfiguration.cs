using EventMaker.Common.Constants;
using EventMaker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EventMaker.DAL.Configurations
{
    /// <summary>
    /// EF configuration for Profile entity
    /// </summary>
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Tags)
             .HasKey(eventparticipants => eventparticipants.Id);

            builder.Property(tag => tag.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);
        }
    }
}
