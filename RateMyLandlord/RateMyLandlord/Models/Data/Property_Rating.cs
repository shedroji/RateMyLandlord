using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.Data
{
    [Table("tblProperty_Ratings")]
    public class Property_Rating
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int pRating { get; set; }
        public string Comment { get; set; }
        public float MonthlyRent { get; set; }
    }
}