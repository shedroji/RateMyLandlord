﻿using System;
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
            this.StreetAddress = row.StreetAddress;
            this.City = row.City;
            this.Country = row.Country;
            this.ZipCode = row.ZipCode;
            this.Rating = row.Rating;
            this.Description = row.Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public int Rating { get; set; }
        public bool UtilitiesIncluded { get; set; }
        public string Description { get; set; }
    }
}