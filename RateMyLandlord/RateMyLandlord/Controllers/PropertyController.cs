using RateMyLandlord.Models.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreatePropertyViewModel newProperty)
        {
            //Validat that required Fields are filled in

            //Create an instance of DBContext

                //Make sure the new Property is Unique by comparing addresses. 

                //Create UserDTO

                //Add to context

                // Save Changes

            //Redirect to Properties page.
            return View();
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
            //Retrieve the property from the DB

                //Populate the PropertyProfileViewModel

            //Return the View with the ViewModel
            return View();
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