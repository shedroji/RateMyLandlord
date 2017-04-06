using log4net;
using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Property;
using RateMyLandlord.Models.ViewModels.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class RateController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RateController));
        private object propId;

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

        [HttpGet]
        public int getUserId(string username)
        {
            int userId;
           
            using(RMLDbContext context = new RMLDbContext())
            {
                User userDTO = context.Users.FirstOrDefault(u=>u.Username.Equals(username));
                userId = userDTO.Id;
            }
            return userId;
        }

        [HttpGet]
        public int getPropertyId(object property)
        {
            int propId;
            using(RMLDbContext context = new RMLDbContext())
            {
               Property propertyDTO = context.Properties.FirstOrDefault(p => p.Id.Equals(property));
                propId = propertyDTO.Id;
            }
            return propId;
        }

        [HttpPost]
        public ActionResult StorePropertyRating(PropertyRatingViewModel rating)
        {
            //Vars
            string username = this.User.Identity.Name;
            
            //validate the rating and user
            //if (rating.pRating != null)
            //{
            //    ModelState.AddModelError("", "Rating must not be Null");
            //    return View();
            //}
            if (!string.IsNullOrWhiteSpace(username))
            {
                int userId = getUserId(username);
                int propertyId = Convert.ToInt32(Session["propertyId"]);

                using (RMLDbContext context = new RMLDbContext())
                {

                    //has the user rated this propery ?
                    if (context.Property_Ratings.Any(r => r.PropertyId.Equals(propertyId)) && context.Property_Ratings.Any(u => u.UserId.Equals(userId)))
                    {
                        ModelState.AddModelError("", "You have already Rated this property");
                        ViewBag.Message = "You have already Rated this property";
                        return View();
                    }
                    //populate the dto 
                    Property_Rating newPropertyRatingDTO = new Property_Rating()
                    {
                        Id = rating.Id,
                        UserId = userId,
                        PropertyId = propertyId,
                        Comment = rating.Comment,
                        pRating = rating.pRating,
                        MonthlyRent = rating.MonthlyRent
                    };
                    newPropertyRatingDTO = context.Property_Ratings.Add(newPropertyRatingDTO);
                    UpdatePropertyRating(rating.pRating, propertyId);
                    // save to the DB
                    context.SaveChanges();
                    log.Info("Rating Saved for property");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please log in before rating.");
                return View("../Account/Login");
            }
            //connect to the DB
            


            //Return the view
            return View("RatingThankYou");
        }

        private void UpdatePropertyRating(int pRating, int propertyId)
        {
            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    Property propertyDTO = context.Properties.Find(propertyId);
                    if(propertyDTO != null)
                    propertyDTO.Rating = pRating;

                    context.SaveChanges();
                    log.Info("Rating saved for: {}" + propertyId);
                }
            }catch(Exception ex)
            {
                log.Error("Failed to update Rating for: {}" + propertyId + "Exception: {}", ex);
            }
        }

        [HttpGet]
        public double getpropertyStarRating(int Id)
        {
            var resultCollection = new List<Property_Rating>();
            PropertyViewModel propertyVM;
            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    int count = 0;
                    double totalRating = 0;
                    double ratingAvg = 0;
                    List<Property_Rating> ratingList = new List<Property_Rating>();
                    resultCollection = context.Property_Ratings.Where(r => r.PropertyId.Equals(Id)).ToList();
                    foreach(var rating in resultCollection)
                    {
                        totalRating = totalRating + rating.pRating; 
                        count++;
                    }
                    ratingAvg = totalRating / count;
                    return ratingAvg;
                }
            } catch(Exception ex)
            {
                log.Error("Could not retreive rating: {}", ex);
                return 0;
            }
        }
    }
}