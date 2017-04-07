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

        public ActionResult Details(int id)
        {
            UserProfileViewModel userVM;
            
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
                    };
                }
                return View(userVM);
            }catch(Exception ex)
            {
                log.Error("Could not load landlord details: {}", ex);
                return View();
            }
        }
    }
}