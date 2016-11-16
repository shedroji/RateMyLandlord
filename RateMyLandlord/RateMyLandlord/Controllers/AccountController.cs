using RateMyLandlord.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult Login(LoginUserViewModel loginUser)
        {
            //Validate username ans password is passed
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Login is required");
                return View();
            }
            if (string.IsNullOrWhiteSpace(loginUser.Username))
            {
                ModelState.AddModelError("", "Username is Required");
                return View();
            }
            if (string.IsNullOrWhiteSpace(loginUser.Password))
            {
                ModelState.AddModelError("", "Password is required");
                return View();
            }
            //open DB connection

            //query for user based on username and password

            //if invalid send error

            //valid, redirect to user profile 
            System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);

            return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));
                 
        }
    }
}