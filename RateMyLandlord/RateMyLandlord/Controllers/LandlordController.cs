using log4net;
using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class LandlordController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PropertyController));

        [HttpGet]
        public int getUserId(string username)
        {
            int userId;

            using (RMLDbContext context = new RMLDbContext())
            {
                User userDTO = context.Users.FirstOrDefault(u => u.Username.Equals(username));
                userId = userDTO.Id;
            }
            return userId;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<UserViewModel> landlordVM = new List<UserViewModel>();

            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    List<User> landlords = context.Users.ToArray().Where(r => r.UserType.Equals("Landlord")).ToList();
                    foreach(var item in landlords)
                    {
                        landlordVM.Add(new UserViewModel(item));
                    }
                }
                return View(landlordVM);
            }catch(Exception ex)
            {
                log.Error("Could not get Landlords: {}", ex);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            UserProfileViewModel userVM;
            double landlordAvg = getAvgRating(id);
            
            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    User user = context.Users.FirstOrDefault(r => r.Id == id);
                    userVM = new UserProfileViewModel()
                    {
                        FirstName = user.FirstName, 
                        LastName = user.LastName, 
                        Username = user.Username, 
                        Rating = landlordAvg
                    };
                }
                Session["landlordId"] = id;
                return View(userVM);
            }catch(Exception ex)
            {
                log.Error("Could not load landlord details: {}", ex);
                return View();
            }
        }

        [HttpPost]
        public ActionResult StoreLandlordRating(LandlordRatingViewModel lRating)
        {
            string username = this.User.Identity.Name;
            Landlord_Rating newLandlordRatingDTO;

            if (!string.IsNullOrWhiteSpace(username))
            {
                int userId = getUserId(username);
                int landlordId = Convert.ToInt32(Session["landlordId"]);
                try
                {
                    using (RMLDbContext context = new RMLDbContext())
                    {
                        if (context.Landlord_Ratings.Any(r => r.UserId.Equals(userId)) && context.Landlord_Ratings.Any(u => u.RaterId.Equals(landlordId)))
                        {
                            ModelState.AddModelError("", "You have already Rated this landlord");
                            ViewBag.Message = "You have already rated this landlord.";
                            return View("Index");
                        }

                        newLandlordRatingDTO = new Landlord_Rating()
                        {
                            Id = lRating.Id,
                            UserId = landlordId,
                            RaterId = userId,
                            Rating = lRating.Rating,
                            Comment = lRating.Comment
                        };

                        newLandlordRatingDTO = context.Landlord_Ratings.Add(newLandlordRatingDTO);
                        context.SaveChanges();
                        log.Info("Landlord Rating Saved");
                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong. Your rating was not processed.");
                    log.Error("Could not add landlord rating: {}", ex);
                    return View();
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Please log in before rating.");
                return View("../Account/Login");
            }
            return View("../Landlord/RatingThankYou");
        }

        [HttpGet]
        public double getAvgRating(int userId)
        {
            double avgRating;
            var resultCollection = new List<Landlord_Rating>();
            //LandlordRatingViewModel landlordRatingVM;

            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    double count = 0;
                    double totalRating = 0;
                    //List<Landlord_Rating> ratingList = new List<Landlord_Rating>();
                    resultCollection = context.Landlord_Ratings.Where(r => r.UserId.Equals(userId)).ToList();
                    foreach(var rating in resultCollection)
                    {
                        totalRating = totalRating + rating.Rating;
                        count++;
                    }
                    avgRating = totalRating / count;
                    return avgRating;
                }
            }catch(Exception ex)
            {
                log.Error("Could not get average rating.");
                ModelState.AddModelError("", "Could not get average rating.");
                return avgRating = 0;
            }

            return avgRating;
        }

        [HttpGet]
        public ActionResult getComments(int id)
        {
            var resultCollection = new List<Landlord_Rating>();
            List<CommentViewModel> landlordVM = new List<CommentViewModel>();
            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    resultCollection = context.Landlord_Ratings.Where(r => r.RaterId.Equals(id)).ToList();
                    foreach(var result in resultCollection)
                    {
                        landlordVM.Add(new CommentViewModel(result));
                    }
                    return View(landlordVM);
                }
            }catch(Exception ex)
            {
                log.Error("Could not get comments.");
                ModelState.AddModelError("", "Could not get comments");
                return View();
            }
            return View();
        }
    }
}