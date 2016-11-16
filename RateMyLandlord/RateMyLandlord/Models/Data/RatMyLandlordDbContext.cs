using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.Data
{
    public class RatMyLandlordDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}