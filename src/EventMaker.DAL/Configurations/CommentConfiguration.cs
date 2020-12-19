using System;
using EventMaker.Common.Constants;
using EventMaker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventMaker.DAL.Configurations
{
    /// <summary>
    /// EF configuration for Comment entity
    /// </summary>
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Comments)
             .HasKey(comment => comment.Id);

            builder.Property(comment => comment.AuthorName)
                .IsRequired()
                .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(comment => comment.MessageText)
               .IsRequired()
               .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.HasOne(comment => comment.Event)
                .WithMany(ev => ev.Comments)
                .HasForeignKey(events => events.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
    


