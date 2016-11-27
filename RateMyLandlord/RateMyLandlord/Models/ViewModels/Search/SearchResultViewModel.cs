using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RateMyLandlord.Models.Data;

namespace RateMyLandlord.Models.ViewModels.Search
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel(Models.Data.Property p)
        {
            this.Id = p.Id;
            this.DisplayText = p.Name;
            this.DisplaySubText = p.Building + " " + p.Unit + " " + p.Street + " " + p.City;
            this.ResultType = nameof(Property);
        }

        public SearchResultViewModel(User u)
        {
            this.Id = u.Id;
            this.DisplayText = u.FirstName + " " + u.LastName;
            this.DisplaySubText = (u.IsLandlord) ? "Landlord" : "";
            this.ResultType = nameof(User);
        }


        public int Id { get; set; }
        public string DisplayText { get; set; }
        public string DisplaySubText { get; set; }
        public string ResultType { get; set; }
    }
}