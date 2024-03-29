﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class CreatePropertyViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code Format")]
        public int ZipCode { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public bool UtilitiesIncluded { get; set; }
        [DisplayName("Pictures")]
        public byte[] ImageContent { get; set; }

        public List<SelectListItem> stateList { get; set; }
    }
}