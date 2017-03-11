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
    public class ConfirmUserViewModel
    {
        public int User_Id { get; set; }
        [Required]
        [DisplayName("Verification Code")]
        public int ValidationCode { get; set; }
        public string Email { get; set; }
    }
}