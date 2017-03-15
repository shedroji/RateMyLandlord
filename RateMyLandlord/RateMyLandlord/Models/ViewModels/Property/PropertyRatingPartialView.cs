using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class PropertyRatingPartialView
    {
        public PropertyRatingPartialView(Data.Property_Rating row)
        {
            this.Id = row.Id;
            this.UserId = row.UserId;
            this.PropertyId = row.PropertyId;
            this.pRating = row.pRating;
            this.Comment = row.Comment;
            this.MonthlyRent = row.MonthlyRent;

        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public double pRating { get; set; }
        public string Comment { get; set; }
        public decimal MonthlyRent { get; set; }
    }
}