using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using RateMyLandlord.Models.Data;

namespace RateMyLandlord.Models.ViewModels.Account
{
    public class CreateUserViewModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        public string PasswordConfirm { get; set; }
       
        [Required]
        [DisplayName("User Type")]
        public string UserType { get; set; }

        public List<Roles> _UserRoles;

        public List<SelectListItem> UserTypes { get; set; }

        [DisplayName("Verification Code")]
        public string VerificationCode { get; set; }


    }
}