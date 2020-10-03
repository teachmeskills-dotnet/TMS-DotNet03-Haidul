using EventMaker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using EventMaker.Common.Constants;

namespace EventMaker.DAL.Configurations
{
    /// <summary>
    /// EF configuration for Profile entity
    /// </summary>
    class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        /// <summary>
        /// inheritdoc.
        /// </summary>
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("Profiles")
             .HasKey(p => p.Id);

            builder.Property(profile => profile.Email)
             .IsRequired()
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.Username)
             .IsRequired()
             .HasMaxLength(ConfigurationContants.SqlMaxLengthShort);

            builder.Property(profile => profile.FirstName)
             .IsRequired()
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
                .HasForeignKey<Profile>(profile => profile.UserId);
        }
    }
}
