using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateMyLandlord.Models.ViewModels.Property
{
    public class CreatePropertyViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Unit { get; set; }

        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Region { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public int Rating { get; set; }

    }
}