namespace RateMyLandlord.Models.ViewModels.Account
{
    public class LandlordRatingViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RaterId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}