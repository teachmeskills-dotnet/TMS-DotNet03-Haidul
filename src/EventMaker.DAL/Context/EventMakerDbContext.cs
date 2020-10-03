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
        /// Apply Configurations for Database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
  