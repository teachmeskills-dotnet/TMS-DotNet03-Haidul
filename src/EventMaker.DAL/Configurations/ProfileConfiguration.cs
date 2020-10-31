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
    internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.Profiles)
             .HasKey(profile => profile.Id);

            builder.Property(profile => profile.Email)
             .IsRequired()
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.Username)
             .IsRequired()
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.FirstName)
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.LastName)
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.Phone)
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.Telegram)
             .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(profile => profile.SocialNetwork)
             .HasMaxLength(ConfigurationContants.SqlMaxLengthMedium);

            builder.Property(profile => profile.BirthDate)
                .HasColumnType(ConfigurationContants.SqlDateFormat);

            builder.HasOne(profile => profile.User)
                .WithOne(user => user.Profile)
                .HasForeignKey<Profile>(profile => profile.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}