﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RateMyLandlord.Models.Data;
using System.ComponentModel;

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
            this.ZipCode = row.ZipCode;
            this.Rating = row.Rating;
            this.UtilitiesIncluded = row.UtilitiesIncluded;
            this.Description = row.Description;
            this.ImageContent = row.ImageContent;
        }

        public PropertyViewModel()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public double Rating { get; set; }
        public bool UtilitiesIncluded { get; set; }
        public string Description { get; set; }
        [DisplayName("Pictures")]
        public byte[] ImageContent { get; set; }

    }
}