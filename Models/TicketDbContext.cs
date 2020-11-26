using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace cw2_ssd.Models
{
    public class TicketDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Connects DB Context to the database init
        /// </summary>
        public TicketDbContext() : base("TicketConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitaliser());
        }

        /// <summary>
        /// Creates an instance of TicketDbContext
        /// </summary>
        /// <returns>TicketDbContext</returns>
        public static TicketDbContext Create()
        {
            return new TicketDbContext();
        }
        
        /// <summary>
        /// Retrieves content from these classes and uses them to add into the database
        /// </summary>
        public System.Data.Entity.DbSet<cw2_ssd.Models.Ticket> Tickets { get; set; }
        public System.Data.Entity.DbSet<cw2_ssd.Models.Comment> Comments { get; set; }
    }
}