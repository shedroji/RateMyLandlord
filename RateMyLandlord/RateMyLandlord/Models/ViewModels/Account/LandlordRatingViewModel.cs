using RateMyLandlord.Models.Data;

namespace RateMyLandlord.Models.ViewModels.Account
{
    public class LandlordRatingViewModel
    {
        private Landlord_Rating result;

        //public LandlordRatingViewModel(Landlord_Rating row)
        //{
        //    this.Id = row.Id;
        //    this.RaterId = row.RaterId;
        //    this.Rating = row.Rating;
        //    this.Comment = row.Comment;
        //}

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RaterId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}