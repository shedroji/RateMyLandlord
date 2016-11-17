using RateMyLandlord.Models.Data;
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel newUser)
        {
            //Validate the new User


            //Check That the required fields are set
            if(!ModelState.IsValid)
            {
                return View(newUser);
            }

            //Check password matches confirmpassword
            if(!newUser.Password.Equals(newUser.PasswordConfirm))
            {
                ModelState.AddModelError("", "Password does not match Password Confirm.");
                return View(newUser);
            }

            //Create an instance of DbContext
            using (RateMyLandlordDbContext context = new RateMyLandlordDbContext())
            {
                //Make sure username is unique
                if(context.Users.Any(row => row.Username.Equals(newUser.Username)))
                {
                    ModelState.AddModelError("", "Username '" + newUser.Username + "'already exists. Try again.");
                    newUser.Username = "";
                    return View(newUser);
                }

                //Create our userDTO
                User newUserDTO = new Models.Data.User()
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    IsActive = true,
                    IsAdmin = false,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };

                //Add to DbContext
                newUserDTO = context.Users.Add(newUserDTO);

                //Save Changes
                context.SaveChanges();
            }

            //Redirect to the Login Page
            return RedirectToAction("login");
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
            bool isValid = false;
            using(RateMyLandlordDbContext context = new RateMyLandlordDbContext())
            {
                //Hash password


                //query for user based on username and password
                if (context.Users.Any(
                    row=>row.Username.Equals(loginUser.Username)
                    && row.Password.Equals(loginUser.Password)
                    ))
                {
                    isValid = true;
                }
            
            }
                //if invalid send error
            if(!isValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            else
            {
                //valid, redirect to user profile 
                System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);

                return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));
            }                
        }
    }
}