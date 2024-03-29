﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class PropertyRatingViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int pRating { get; set; }
        public string Comment { get; set; }
        public decimal MonthlyRent { get; set; }
    }
}