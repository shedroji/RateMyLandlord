using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RateMyLandlord.Models.Data;

namespace RateMyLandlord.Models.ViewModels.Account
{
    public class UserViewModel
    {
        private User item;

        public UserViewModel(User item)
        {
            this.Id = item.Id;
            this.FirstName = item.FirstName;
            this.LastName = item.LastName;
            this.Username = item.Username;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

    }
}