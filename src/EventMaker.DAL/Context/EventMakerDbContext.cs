using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventMaker.DAL.Context
{
    /// <summary>
    /// Database context.
    /// </summary>
    public  class EventMakerDbContext : IdentityDbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public EventMakerDbContext(DbContextOptions<EventMakerDbContext> options)
            : base(options) { }
         
    }
}
  