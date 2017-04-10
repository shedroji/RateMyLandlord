using RateMyLandlord.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Account
{
    public class CommentViewModel
    {
        public CommentViewModel(Landlord_Rating row)
        {
            this.Id = row.Id;
            this.UserId = row.UserId;
            this.RaterId = row.RaterId;
            this.Rating = row.Rating;
            this.Comment = row.Comment;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RaterId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}