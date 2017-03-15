using log4net;
using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Property;
using RateMyLandlord.Models.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*
* Create a way to store comments and a star rating system in another controller - Jake.
*/

namespace RateMyLandlord.Controllers
{
    public class PropertyController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PropertyController));

        // GET: Property
        public ActionResult Index()
        {
            List<PropertyViewModel> propertyVM;
            using(RMLDbContext context = new RMLDbContext())
            {
                propertyVM = context.Properties.ToArray().Select(x => new PropertyViewModel(x)).ToList();

            }
            return View(propertyVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreatePropertyViewModel newProperty)
        {
            //Validate that required Fields are filled in
            if (!ModelState.IsValid)
            {
                return View(newProperty);
            }

            //Create an instance of DBContext
            try
            {
                using (RMLDbContext context = new RMLDbContext())
                {
                    //Make sure the new Property is Unique by comparing addresses. 
                        if(context.Properties.Any(row=>row.City.Equals(newProperty.City)) && 
                            context.Properties.Any(row=>row.Country.Equals(newProperty.Country)) && 
                            context.Properties.Any(row=>row.ZipCode.Equals(newProperty.ZipCode))
                        )
                        {
                            ModelState.AddModelError("", "This Property already exists.");
                            return View();
                        }
                    //Create propertyDTO
                    Property newPropertyDTO = new Models.Data.Property()
                    {
                        Name = newProperty.Name,
                        StreetAddress = newProperty.StreetAddress,
                        City = newProperty.City,
                        State = newProperty.State,
                        Country = newProperty.Country,
                        ZipCode = newProperty.ZipCode,
                        Rating = newProperty.Rating,  
                        Description = newProperty.Description,
                        UtilitiesIncluded = newProperty.UtilitiesIncluded
                    };
                    //Add to context
                    newPropertyDTO = context.Properties.Add(newPropertyDTO);
                    // Save Changes
                    context.SaveChanges();
                }
                log.Info("Property created successfully : " + newProperty.Name);
            } catch (Exception ex)
            {
                log.Error("Failed to create property: " + newProperty.Name + " : {}",ex );
            }

                //Redirect to Properties page.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            //Get property by ID
            EditViewModel editVM;
            //Retrieve the property from the DB
            using (RMLDbContext context = new RMLDbContext())
            {
                //Populate the PropertyProfileViewModel
                var rating = new RateController().getpropertyStarRating(Id);
                Property propertyDTO = context.Properties.FirstOrDefault(x => x.Id == Id);
                if (propertyDTO == null)
                {
                    ModelState.AddModelError("", "Invalid Property Id");
                }
                //Create Edit VM
                editVM = new EditViewModel()
                {
                    Id = propertyDTO.Id,
                    Name = propertyDTO.Name,
                    StreetAddress = propertyDTO.StreetAddress,
                    City = propertyDTO.City,
                    State = propertyDTO.State,
                    Country = propertyDTO.Country,
                    ZipCode = propertyDTO.ZipCode,
                    Rating = rating,
                    UtilitiesIncluded = propertyDTO.UtilitiesIncluded,
                    Description = propertyDTO.Description
                };
            }

            //Send VM to the view

            return View(editVM);
        }
        
        [HttpPost]
        public ActionResult Update(EditViewModel editVM)
        {
            //Variables
            Property propertyDTO;
            using (RMLDbContext context = new RMLDbContext())
            {
                //Get property from DB
                var rating = new RateController().getpropertyStarRating(editVM.Id);
                propertyDTO = context.Properties.Find(editVM.Id);
                if (propertyDTO == null) { return Content("Invalid User Id."); }

                //Set/ Update values from the view model
                propertyDTO.Name = editVM.Name;
                propertyDTO.StreetAddress = editVM.StreetAddress;
                propertyDTO.City = editVM.City;
                propertyDTO.State = editVM.State;
                propertyDTO.ZipCode = editVM.ZipCode;
                propertyDTO.Rating = rating;
                propertyDTO.UtilitiesIncluded = editVM.UtilitiesIncluded;
                propertyDTO.Description = editVM.Description;

                //Save Changes
                try
                {
                    context.SaveChanges();
                    log.Info("Property " + propertyDTO.Name + " updated!");
                }
                catch (Exception ex)
                {
                    log.Error("Could not get Property to edit : {}", ex);
                }
            }

            //Return user to property page

            return RedirectToAction("Index");
        }

        public ActionResult PropertyProfile(int Id)
        {
            PropertyProfileViewModel propertyVM;
            //Retrieve the property from the DB
            using(RMLDbContext context = new RMLDbContext())
            {
                var rating = new RateController().getpropertyStarRating(Id);
                //Populate the PropertyProfileViewModel
                Property propertyDTO = context.Properties.FirstOrDefault(x => x.Id == Id);
                if(propertyDTO == null)
                {
                    ModelState.AddModelError("", "Invalid Property Id");
                }
                propertyVM = new PropertyProfileViewModel()
                {
                    Id = propertyDTO.Id,
                    Name = propertyDTO.Name,
                    StreetAddress = propertyDTO.StreetAddress, 
                    City = propertyDTO.City,
                    State = propertyDTO.State, 
                    Country = propertyDTO.Country, 
                    ZipCode = propertyDTO.ZipCode, 
                    Rating = rating,
                    UtilitiesIncluded = propertyDTO.UtilitiesIncluded, 
                    Description = propertyDTO.Description
                };
            }
            int propertyId = Id;
            Session["propertyId"] = propertyId;
            //Return the View with the ViewModel
            return View(propertyVM);
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            List<SearchResultViewModel> resultVMCollection = new List<SearchResultViewModel>();
            using (RMLDbContext context = new RMLDbContext())
            {
                IQueryable<Property> propertyResults = context.Properties
                    .Where(p =>
                        p.Name.Contains(query) ||
                        p.City.Contains(query)
                    );

                foreach (var item in propertyResults)
                {
                    resultVMCollection.Add(new SearchResultViewModel(item));
                }

                IQueryable<User> userResults = context.Users
                    .Where(u =>
                        u.FirstName.Contains(query) ||
                        u.LastName.Contains(query) ||
                        u.Username.Contains(query) ||
                        u.Email.Contains(query)
                    );

                foreach(var item in userResults)
                {
                    resultVMCollection.Add(new SearchResultViewModel(item));
                }
            }

            return View(resultVMCollection);
        }

        [HttpGet]
        public ActionResult GetAllProperties()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetIndividualProperties()
        {
            return View();
        }
    }
}