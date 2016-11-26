using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Models.ViewModels.Account
{
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirm { get; set; }

        [Required]
        public string AccountType { get; set; }

        public bool RecieveEmailNewsletter { get; set; }
        public bool IsLandlord { get; internal set; }
        public int LandlordId { get; internal set; }
    }
}