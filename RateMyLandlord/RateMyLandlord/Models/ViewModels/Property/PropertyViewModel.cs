using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RateMyLandlord.Models.Data;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class PropertyViewModel
    {

        public PropertyViewModel(Data.Property row)
        {
            this.Id = row.Id;
            this.Name = row.Name;
            this.City = row.City;
            this.Country = row.Country;
            this.ZipCode = row.ZipCode;
            this.Rating = row.Rating;
        }

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