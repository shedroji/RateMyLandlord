using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace RateMyLandlord.Models.Data
{
    [Table("tblProperty_Images")]
    public class PropertyImages
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImageId { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }
        public int Size { get; set; }
        public byte[] ImageContent { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ImagePath { get; set; }
    }
}