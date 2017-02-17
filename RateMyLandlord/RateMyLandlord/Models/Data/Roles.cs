using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RateMyLandlord.Models.Data
{
    [Table("tblRoles")]
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLandlord { get; set; }
        public bool IsModerator { get; set; }
    }
}