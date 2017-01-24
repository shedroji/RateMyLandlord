using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Property;
using RateMyLandlord.Models.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.Sql;

/*
* Create a way to store comments and a star rating system in another controller.
*/

namespace RateMyLandlord.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public ActionResult Index()
        {
            List<PropertyViewModel> propertyVM;
            using(RateMyLandlordDbContext context = new RateMyLandlordDbContext())
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
            using (RateMyLandlordDbContext context = new RateMyLandlordDbContext())
            {
                //Make sure the new Property is Unique by comparing addresses. 
                    if(context.Properties.Any(row=>row.Unit.Equals(newProperty.Unit)) && 
                        context.Properties.Any(row=>row.Building.Equals(newProperty.Building)) && 
                        context.Properties.Any(row=>row.Street.Equals(newProperty.Street)) && 
                        context.Properties.Any(row=>row.City.Equals(newProperty.City)) && 
                        context.Properties.Any(row=>row.Region.Equals(newProperty.Region)) &&
                        context.Properties.Any(row=>row.Country.Equals(newProperty.Country)) && 
                        context.Properties.Any(row=>row.ZipCode.Equals(newProperty.ZipCode))
                    )
                    {
                        ModelState.AddModelError("", "This Property already exists.");
                        return View();
                    }
                //Create UserDTO
                Property newPropertyDTO = new Models.Data.Property()
                {
                    Name = newProperty.Name,
                    Unit = newProperty.Unit,
                    Building = newProperty.Building,
                    Street = newProperty.Street,
                    City = newProperty.City,
                    Region = newProperty.Region,
                    Country = newProperty.Country,
                    ZipCode = newProperty.ZipCode,
                    Rating = newProperty.Rating, 
                    RatingDescription = newProperty.RatingDescription, 
                    Description = newProperty.Description
                };
                //Add to context
                newPropertyDTO = context.Properties.Add(newPropertyDTO);
                // Save Changes
                context.SaveChanges();
            }

                //Redirect to Properties page.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update()
        {
            //Get property by ID

            //Create Edit VM

            //Send VM to the view

            return View();
        }
        
        [HttpPost]
        public ActionResult Update(UpdateViewModel updateVM)
        {
            //Variables

            //Validate the model

            //Get property from DB

                //Set / Update values from the view Model

                //Save Changes

            //Return user to property page
            
            return View();
        }

        public ActionResult PropertyProfile(int Id)
        {
            PropertyProfileViewModel propertyVM;
            //Retrieve the property from the DB
            using(RateMyLandlordDbContext context = new RateMyLandlordDbContext())
            {
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
                    Unit = propertyDTO.Unit, 
                    Building = propertyDTO.Building, 
                    Street = propertyDTO.Street, 
                    City = propertyDTO.City, 
                    Region = propertyDTO.Region, 
                    Country = propertyDTO.Country, 
                    ZipCode = propertyDTO.ZipCode, 
                    Rating = propertyDTO.Rating, 
                    RatingDescription = propertyDTO.RatingDescription,
                    Description = propertyDTO.Description

                };
            }

            //Return the View with the ViewModel
            return View(propertyVM);
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            List<SearchResultViewModel> resultVMCollection = new List<SearchResultViewModel>();
            using (RateMyLandlordDbContext context = new RateMyLandlordDbContext())
            {
                IQueryable<Property> propertyResults = context.Properties
                    .Where(p =>
                        p.Name.Contains(query) ||
                        p.Unit.Contains(query) ||
                        p.Building.Contains(query) ||
                        p.Street.Contains(query) ||
                        p.City.Contains(query) ||
                        p.Region.Contains(query)
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