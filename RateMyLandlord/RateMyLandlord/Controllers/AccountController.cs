using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //Validate username ans password is passed
            
            //open DB connection
            
            //query for user based on username and password

            //if invalid send error
            
            //valid, redirect to user profile            
        }
    }
}