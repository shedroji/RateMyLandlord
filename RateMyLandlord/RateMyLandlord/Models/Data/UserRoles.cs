using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace RateMyLandlord.Models.Data
{
    [Table("tblUser_Roles")]
    public class UserRoles
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}