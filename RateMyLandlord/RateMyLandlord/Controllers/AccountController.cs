using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace RateMyLandlord.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AccountController));
        List<SelectListItem> userTypesList = new List<SelectListItem>();

        // GET: Account
        public ActionResult Index()
        {
            //userTypesList.Add(new SelectListItem() { Text = "Tenant", Value = "Tenant" });
            //userTypesList.Add(new SelectListItem() { Text = "Landlord", Value = "Landlord" });
            return this.RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Create()
        {
            CreateUserViewModel cuvm = new CreateUserViewModel();
            userTypesList.Add(new SelectListItem() { Text = "Tenant", Value = "Tenant" });
            userTypesList.Add(new SelectListItem() { Text = "Landlord", Value = "Landlord" });
            cuvm.UserTypes = userTypesList;
            return View(cuvm);
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel newUser)
        {
            //newUser.UserTypes = userTypesList;

            Random randomCode = new Random();
            int authCode = randomCode.Next(1000000);
           // string authCode = randNum.ToString("D6");

            //Validate the new User

            //Check That the required fields are set
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            //Check password matches confirmpassword
            if (!newUser.Password.Equals(newUser.PasswordConfirm))
            {
                ModelState.AddModelError("", "Password does not match Password Confirm.");
                return View(newUser);
            }
            try
            {

                string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(newUser.Password, "MD5");

                //Create an instance of DbContext
                using (RMLDbContext context = new RMLDbContext())
                {
                    //Make sure username is unique
                    if (context.Users.Any(row => row.Username.Equals(newUser.Username)))
                    {
                        ModelState.AddModelError("", "Username '" + newUser.Username + "'already exists. Try again.");
                        newUser.Username = "";
                        return View(newUser);
                    }

                    //Create our userDTO
                    User newUserDTO = new User()
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Username = newUser.Username,
                        Email = newUser.Email,
                        Password = hashedPassword,
                        IsActive = true,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        UserType = newUser.UserType,
                        EmailConfirmed = false
                    };

                    // get the max user id from the db and increment it by 1 to get the userID auto increment value from newUserDTO
                    int newUserId = (int?)context.Users.Max(u => u.Id) + 1 ?? 1; 

                    //Create validationDTO
                    UserValidation newUserValidation = new UserValidation()
                    {
                        User_Id = newUserId,
                        ValidationCode = authCode,
                        DateCreated = newUserDTO.DateCreated //done this way to ensure the date and time are the same
                    };


                    //Add to DbContext
                    newUserDTO = context.Users.Add(newUserDTO);
                    newUserValidation = context.UserValidation.Add(newUserValidation);

                    //Save Changes
                    context.SaveChanges();

                    SendEmailConfirmation(newUser.FirstName, newUserId, newUser.Email, authCode);
                    
                    return RedirectToAction("Confirm", "Account", new { Email = newUser.Email, Id = newUserId });
                    //return RedirectToAction("SendEmailConfirmation", new { firstName = newUser.FirstName, userID = newUserId, emailAddress = newUser.Email, token = authCode });
                }

            }
            catch (Exception ex)
            {
                // temporary logging
                log.Error("Error creating user: " + newUser.FirstName + ": {}", ex);
            }
            // if we made it this far something went wrong
            userTypesList.Add(new SelectListItem() { Text = "Tenant", Value = "Tenant" });
            userTypesList.Add(new SelectListItem() { Text = "Landlord", Value = "Landlord" });
            newUser.UserTypes = userTypesList;
            return View(newUser);
        }

        /// <summary>
        /// Send email confirmation link.
        /// Sets isEmailConfirmed bool to 1 if confirmed
        /// This method will be called in our Create method for creating a new user
        /// </summary>
        public void SendEmailConfirmation(string firstName, int userID, string emailAddress, int token)
        {
            //int userIdCopy = userID;
            try
            {
                //Create email message object
                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

                // populate message
                email.To.Add(emailAddress);
                email.From = new System.Net.Mail.MailAddress("ratemylandlord03@gmail.com");
                email.Subject = "Please Verify Your Email";
                email.Body = string.Format(
                    "Hey {0}!\r\n {1}",
                    firstName,  
                    "Please enter this code to validate your account " + token.ToString() + "."
                    );
                email.IsBodyHtml = false;

                //set up SMTP client
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential("ratemylandlord03@gmail.com", "p@ssw0rd1");
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                //send message
                //ADD ERROR HANDLING IF EMAIL SEND DOESN'T WORK
                smtpClient.Send(email);
            }
            catch (Exception ex)
            {
                // temporary loggingW
                log.Error("Error sending email to: " + emailAddress + " : {}", ex);
                ModelState.AddModelError(string.Empty, "There was an error sending the email. Please re-enter your email address and try again");
            }

        }

        [HttpGet]
        public ActionResult Confirm(string Email, int Id)
        {
            ViewBag.Title = "Success!";
            ViewBag.Email = Email;
            ViewBag.HideNavBar = true;
            ConfirmUserViewModel user = new ConfirmUserViewModel()
            {
                User_Id = Id,
                Email = Email
            };
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Confirm(ConfirmUserViewModel validatedUser)
        {
            //this code is executed on submit button click
            try
            {
                User dbUser = new User();
                using (RMLDbContext context = new RMLDbContext())
                {
                    UserValidation confirmedUser = context.UserValidation.Where(x => x.ValidationCode == validatedUser.ValidationCode).FirstOrDefault();

                    if (confirmedUser != null)
                    {
                        // A user was returned, meaning the user id and token were valid. Make user Active in db, log them in, and redirect to login page
                        dbUser = context.Users.Where(w => w.Id == confirmedUser.User_Id).FirstOrDefault();
                        if (dbUser != null)
                        {
                            //set EmailConfirmed field to true, stage context for update, and save changes
                            dbUser.EmailConfirmed = true;
                            context.Entry(dbUser).State = System.Data.Entity.EntityState.Modified;

                            //Save Changes
                            context.SaveChanges();
                        }
                    }
                }
                //valid, redirect to user profile 
                return RedirectToAction("Login", "Account", new { message = "Success!" });
            }

            catch(Exception ex)
            {
                // temporary logging
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login(string message = "")
        {
            if(message == string.Empty)
            {
                message = "Login";
            }
            
            ViewBag.Title = message;
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
            try
            {
                using(RMLDbContext context = new RMLDbContext())
                {
                    //Hash password
                    string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(loginUser.Password, "MD5");

                    //query for user based on username and password
                    if (context.Users.Any(
                        row=>row.Username.Equals(loginUser.Username)
                        && row.Password.Equals(hashedPassword)
                        ))
                    {
                        isValid = true;
                    }
                }
                
            } catch (Exception ex)
            {
                log.Error("Failed to Log in. {}", ex);
            }
            //if invalid send error
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            else
            {
                //valid, redirect to user profile 
                System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);
                log.Info(loginUser.Username + " logged in.");
                return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));
            }
        }

        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
            } catch (Exception ex)
            {
                log.Error("Failed to log user out : {}", ex);
            }
            return RedirectToAction("Login");
        }

        public ActionResult UserNavPartial()
        {
            //Capture logged in user
            string username;
            username = this.User.Identity.Name;
            UserNavPartialViewModel userNavVM;
            //Get info from db
            using(RMLDbContext context = new RMLDbContext())
            {
                //Search for User
                Models.Data.User userDTO = context.Users.FirstOrDefault(x => x.Username == username);
                
                if(userDTO == null) { return Content(""); }

                //Build Partial view
                userNavVM = new UserNavPartialViewModel
                {
                    Username = userDTO.Username,
                    Id = userDTO.Id
                };
            }

            //Send the View model 
            return PartialView(userNavVM);
        }

        public ActionResult UserProfile()
        {
            //Capture logged in user
            string username = User.Identity.Name;

            //Retrieve the User from the DB
            UserProfileViewModel profileVM;
            using(RMLDbContext context = new RMLDbContext())
            {
                User userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                if(userDTO == null)
                {
                    return Content("Invalid Username");
                }

                //Populate the UserProfileViewModel
                profileVM = new UserProfileViewModel()
                {
                    Id = userDTO.Id,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Username = userDTO.Username,
                    //IsAdmin = userDTO.IsAdmin,
                    //IsLandlord = userDTO.IsAdmin
                };

            }

            //Retrun the View with the viewModel
            return View(profileVM);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get User by ID
            EditViewModel editVM;
            using(RMLDbContext context = new RMLDbContext())
            {
                //Get User from DB
                User userDTO = context.Users.Find(id);
                if(userDTO == null)
                {
                    return Content("Invalid Id");
                }

                //Create EditVM
                editVM = new EditViewModel
                {
                    Username = userDTO.Username, 
                    FirstName = userDTO.FirstName, 
                    LastName = userDTO.LastName,
                    Email = userDTO.Email, 
                    Id = userDTO.Id
                };

            }



            //Send VM to the View
            return View(editVM);

        }

        [HttpPost]
        public ActionResult Edit(EditViewModel editVM)
        {
            //Variables
            bool needsPasswordReset = false;
            bool usernameHasChanged = false;
            //Validate the model 
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }

            //Check for password change
            if (!string.IsNullOrWhiteSpace(editVM.Password))
            {
                //Compare password with password confirm
                if (editVM.Password != editVM.PasswordConfrim)
                {
                    ModelState.AddModelError("", "Password and Confrim Password Must Match.");
                    return View(editVM);
                }
                else
                {
                    needsPasswordReset = true;
                }
            }


            //Get user from DB
            User userDTO;
            using (RMLDbContext context = new RMLDbContext())
            {
                userDTO = context.Users.Find(editVM.Id);
                if (userDTO == null) { return Content("Invalid User Id."); }

                //Check for Username Change
                if(userDTO.Username != editVM.Username)
                {
                    userDTO.Username = editVM.Username;
                    usernameHasChanged = true;
                }

                //Set/ Update values from the view model
                userDTO.FirstName = editVM.FirstName;
                userDTO.LastName = editVM.LastName;


                if (needsPasswordReset)
                {
                    userDTO.Password = editVM.Password;
                }

                //Save Changes
                try {
                    context.SaveChanges();
                } catch (Exception ex)
                {
                    log.Error("Could not get Account to edit : {}", ex);
                }
            }
            if(usernameHasChanged || needsPasswordReset)
            {
                TempData["LogoutMessage"] = "After a username or password change, please log in with the new credentials.";
                return RedirectToAction("Logout");
            }
            else
            {
                return RedirectToAction("UserProfile");

            }
        }
    }
}