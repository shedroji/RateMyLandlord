using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.Data
{
    public class RMLDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<UserValidation> UserValidation { get; set; }
        public DbSet<Property_Rating> Property_Ratings { get; set; }
    }
}