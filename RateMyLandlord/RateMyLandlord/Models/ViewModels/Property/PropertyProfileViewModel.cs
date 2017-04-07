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
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public bool UtilitiesIncluded { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImagePath { get; set; }
    }
}