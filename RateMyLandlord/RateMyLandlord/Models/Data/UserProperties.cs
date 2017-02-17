using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.Data
{
    [Table("tblUser_Properties")]
    public class UserProperties
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PropertyId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public decimal MonthlyRent { get; set; }
    }
}