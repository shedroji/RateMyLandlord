using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class PropertyRatingPartialView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public double MonthlyRent { get; set; }
    }
}