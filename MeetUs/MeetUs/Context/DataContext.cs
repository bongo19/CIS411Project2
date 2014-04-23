using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MeetUs.Models;

namespace MeetUs.Context
{

        public class DataContext : DbContext
        {
            public DataContext()
                : base("DefaultConnection")
            {
            }

            public DbSet<UserProfile> UserProfiles { get; set; }
            public DbSet<Spot> Spots { get; set; }
        }
    
}