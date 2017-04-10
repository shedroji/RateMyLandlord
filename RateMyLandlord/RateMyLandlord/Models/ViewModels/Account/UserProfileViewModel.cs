using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLandlord { get; set; }
        public double Rating { get; set; }
    }
}