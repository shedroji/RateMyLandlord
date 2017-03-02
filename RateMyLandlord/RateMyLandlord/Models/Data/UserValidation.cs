using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.Data
{
    [Table("tblUser_Validation")]
    public class UserValidation
    {
        [Key]
        public int UserId { get; set; }
        public int ValidationCode { get; set; }
        public DateTime DateCreated { get; set; }
    }
}