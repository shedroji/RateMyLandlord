using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class RateController : Controller
    {
        // GET: RAte
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getPropertyId()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StorePropertyRating(PropertyRatingViewModel rating)
        {
            //Vars
            string username = this.User.Identity.Name;
            //validate the rating and user
            if (rating.pRating != null)
            //{
            //    ModelState.AddModelError("", "Rating must not be Null");
            //    return View();
            //}
            if (string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("", "Please log in before rating.");
                return View("Account/Login");
            }

            //connect to the DB
            using (RMLDbContext context = new RMLDbContext())
            {

                //has the user rated this propery?
                //if (context.Property_Ratings.Any(r => r.PropertyId.Equals(rating.PropertyId)) && context.Property_Ratings.Any(u => u.UserId.Equals(rating.UserId)))
                //{
                //    ModelState.AddModelError("", "YOu have already Rated this property");
                //    return View();
                //}
                //populate the dto 
                Property_Rating newPropertyRatingDTO = new Property_Rating()
                {
                    UserId = rating.UserId,
                    PropertyId = rating.PropertyId,
                    Comment = rating.Comment,
                    pRating = rating.pRating,
                    MonthlyRent = rating.MonthlyRent
                };
                newPropertyRatingDTO = context.Property_Ratings.Add(newPropertyRatingDTO);
                // save to the DB
                context.SaveChanges();
            }


            //Return the view
            return View();
        }
    }
}