using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class PropertyProfileViewModel
    {
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
    }
}