﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.Data
{
    [Table("tblProperty")]
    public class Property
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int Rating { get; set; }
        public string RatingDescription { get; set; }
        public string Description { get; set; }
    }
}