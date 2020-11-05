using EventMaker.DAL.Configurations;
using EventMaker.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventMaker.DAL.Context
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class EventMakerDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public EventMakerDbContext(DbContextOptions<EventMakerDbContext> options)
            : base(options) { }

        /// <summary>
        /// Profiles.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Events.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Event participants.
        /// </summary>
        public DbSet<EventParticipant> EventParticipants { get; set; }

        /// <summary>
        /// Event participants.
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Event participants.
        /// </summary>
        public DbSet<EventTag> EventTags { get; set; }

        /// <summary>
        /// Apply Configurations for Database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new EventTagConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}