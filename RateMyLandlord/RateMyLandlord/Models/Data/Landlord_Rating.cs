using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyLandlord.Models.Data
{
    [Table("tblLandlord_Ratings")]
    public class Landlord_Rating
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RaterId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

    }
}