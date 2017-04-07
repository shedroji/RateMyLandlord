using log4net;
using RateMyLandlord.Models.Data;
using RateMyLandlord.Models.ViewModels.Property;
using RateMyLandlord.Models.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
        List<SelectListItem> listOfStates = new List<SelectListItem>();

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

        //[HttpGet]
        //public double getRating(int Id)
        //{
        //    double rating;
        //    using(RMLDbContext context = new RMLDbContext())
        //    {

        //    }
        //    return rating;
        //}

        [HttpGet]
        public ActionResult Create()
        {
            CreatePropertyViewModel emptyProperty = new CreatePropertyViewModel();
            listOfStates.Add(new SelectListItem() { Text = "Alabama", Value = "AL" });
            listOfStates.Add(new SelectListItem() { Text = "Alaska", Value = "AK" });
            listOfStates.Add(new SelectListItem() { Text = "Arizona", Value = "AZ" });
            listOfStates.Add(new SelectListItem() { Text = "Arkansas", Value = "AR" });
            listOfStates.Add(new SelectListItem() { Text = "California", Value = "CA" });
            listOfStates.Add(new SelectListItem() { Text = "Colorado", Value = "CO" });
            listOfStates.Add(new SelectListItem() { Text = "Connecticut", Value = "CT" });
            listOfStates.Add(new SelectListItem() { Text = "District of Culumbia", Value = "DC" });
            listOfStates.Add(new SelectListItem() { Text = "Delaware", Value = "DE" });
            listOfStates.Add(new SelectListItem() { Text = "Florida", Value = "FL" });
            listOfStates.Add(new SelectListItem() { Text = "Georgia", Value = "GA" });
            listOfStates.Add(new SelectListItem() { Text = "Hawaii", Value = "HI" });
            listOfStates.Add(new SelectListItem() { Text = "Idaho", Value = "ID" });
            listOfStates.Add(new SelectListItem() { Text = "Illinois", Value = "IL" });
            listOfStates.Add(new SelectListItem() { Text = "Indiana", Value = "IN" });
            listOfStates.Add(new SelectListItem() { Text = "Iowa", Value = "IA" });
            listOfStates.Add(new SelectListItem() { Text = "Kansas", Value = "KS" });
            listOfStates.Add(new SelectListItem() { Text = "Kentucky", Value = "KY" });
            listOfStates.Add(new SelectListItem() { Text = "Louisiana", Value = "LA" });
            listOfStates.Add(new SelectListItem() { Text = "Maine", Value = "ME" });
            listOfStates.Add(new SelectListItem() { Text = "Maryland", Value = "MD" });
            listOfStates.Add(new SelectListItem() { Text = "Massachusetts", Value = "MA" });
            listOfStates.Add(new SelectListItem() { Text = "Michigan", Value = "MI" });
            listOfStates.Add(new SelectListItem() { Text = "Minnesota", Value = "MN" });
            listOfStates.Add(new SelectListItem() { Text = "Mississippi", Value = "MS" });
            listOfStates.Add(new SelectListItem() { Text = "Missouri", Value = "MO" });
            listOfStates.Add(new SelectListItem() { Text = "Montana", Value = "MT" });
            listOfStates.Add(new SelectListItem() { Text = "Nebraska", Value = "NE" });
            listOfStates.Add(new SelectListItem() { Text = "Nevada", Value = "NV" });
            listOfStates.Add(new SelectListItem() { Text = "New Hampshire", Value = "NH" });
            listOfStates.Add(new SelectListItem() { Text = "New Jersey", Value = "NJ" });
            listOfStates.Add(new SelectListItem() { Text = "New Mexico", Value = "NM" });
            listOfStates.Add(new SelectListItem() { Text = "New York", Value = "NY" });
            listOfStates.Add(new SelectListItem() { Text = "North Carolina", Value = "NC" });
            listOfStates.Add(new SelectListItem() { Text = "North Dakota", Value = "ND" });
            listOfStates.Add(new SelectListItem() { Text = "Ohio", Value = "OH" });
            listOfStates.Add(new SelectListItem() { Text = "Oklahoma", Value = "OK" });
            listOfStates.Add(new SelectListItem() { Text = "Oregon", Value = "OR" });
            listOfStates.Add(new SelectListItem() { Text = "Pennsylvania", Value = "PA" });
            listOfStates.Add(new SelectListItem() { Text = "Rhode Island", Value = "RI" });
            listOfStates.Add(new SelectListItem() { Text = "South Carolina", Value = "SC" });
            listOfStates.Add(new SelectListItem() { Text = "South Dakota", Value = "SD" });
            listOfStates.Add(new SelectListItem() { Text = "Tennessee", Value = "TN" });
            listOfStates.Add(new SelectListItem() { Text = "Texas", Value = "TX" });
            listOfStates.Add(new SelectListItem() { Text = "Utah", Value = "UT" });
            listOfStates.Add(new SelectListItem() { Text = "Vermont", Value = "VT" });
            listOfStates.Add(new SelectListItem() { Text = "Virginia", Value = "VA" });
            listOfStates.Add(new SelectListItem() { Text = "Washington", Value = "WA" });
            listOfStates.Add(new SelectListItem() { Text = "West Virginia", Value = "WV" });
            listOfStates.Add(new SelectListItem() { Text = "Wisconsin", Value = "WI" });
            listOfStates.Add(new SelectListItem() { Text = "Wyoming", Value = "WY" });

            emptyProperty.stateList = listOfStates;
            return View(emptyProperty);
        }

        //IEnumerable<HttpPostedFileBase>
        [HttpPost]
        public ActionResult Create(CreatePropertyViewModel newProperty, HttpPostedFileBase file)
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
                            context.Properties.Any(row=>row.ZipCode.Equals(newProperty.ZipCode)) &&
                            context.Properties.Any(row => row.StreetAddress.Equals(newProperty.StreetAddress))
                        )
                        {
                            ModelState.AddModelError("", "This Property already exists.");
                            return View();
                        }
                    //int filesNum = Request.Files.Count;
                    //var upload = Request.Files["file"];
                    //if (upload.ContentLength > 0)
                    //{

                    //}

                    //check if an image was uploaded. If so, save it to db
                    if (file != null && file.ContentLength > 0)
                    {
                        //foreach (var image in file)
                        //{
                        //    var serverPath = Server.MapPath("~/files/" + image.FileName);
                        //    image.SaveAs(serverPath);
                        //    AddImages(newProperty, image);
                        //    }
                        string filename = file.FileName;
                        byte[] imageBytes = new byte[file.ContentLength];
                        //newProperty.ImageContent = AddImages(newProperty, imageBytes, filename);
                        newProperty.ImageContent = imageBytes;
                        file.InputStream.Read(newProperty.ImageContent, 0, file.ContentLength);
                        //file.SaveAs(HttpContext.Server.MapPath("~/Images/") + file.FileName);

                        int propImgId = context.Properties.Max(x => (int) x.Id);
                        //string propImgString = context.Properties.OrderByDescending(x => x.Id).FirstOrDefault().ToString();
                        //bool imgConvertResult = Int32.TryParse(propImgString, out propImgId);
                        
                        //create DTO
                        PropertyImages newPropertyImageDTO = new PropertyImages()
                        {
                            //ImageId = context.PropertyImages.Max(p => p.ImageId + 1),
                            PropertyId = propImgId,
                            ImagePath = filename,
                           //UserId = newProperty.UserId,
                            Size = file.ContentLength,
                            ImageContent = newProperty.ImageContent,
                            Active = true,
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now
                        };
                        //Add to context
                        newPropertyImageDTO = context.PropertyImages.Add(newPropertyImageDTO);

                    }
                    //Create propertyDTO
                    Property newPropertyDTO = new Property()
                    {
                        Name = newProperty.Name,
                        StreetAddress = newProperty.StreetAddress,
                        City = newProperty.City,
                        State = newProperty.State,
                        ZipCode = newProperty.ZipCode,
                        Rating = newProperty.Rating,  
                        Description = newProperty.Description,
                        UtilitiesIncluded = newProperty.UtilitiesIncluded,
                        ImageContent = newProperty.ImageContent,
                        ImagePath = file.FileName

                    };

                    

                    //Add to context
                    newPropertyDTO = context.Properties.Add(newPropertyDTO);
                    // Save Changes
                    //file.SaveAs(file.FileName);
                    context.SaveChanges();
                    ViewBag.Message = "File uploaded successfully";
                }
                log.Info("Property created successfully : " + newProperty.Name);
            } catch (Exception ex)
            {
                log.Error("Failed to create property: " + newProperty.Name + " : {}",ex );
            }

                //Redirect to Properties page.
            return RedirectToAction("Index");
        }

        /// <summary>
        /// adds an image to a property
        /// </summary>
        /// <param name="image"></param>
        public byte[] AddImages(CreatePropertyViewModel property, byte[] image, string imageFileName)
        {
            //not convinced we need a separate method for this, but possibly.
            //on that youtube video, his AddImage is basically our Create
            //property.ImageContent = new byte[image.ContentLength];
            //image.InputStream.Read(property.ImageContent, 0, image.ContentLength);

            //Save image to file
            string filename = imageFileName;
            string filePathOriginal = Server.MapPath("/Content/Uploads/Originals");
            string filePathThumbnail = Server.MapPath("/Content/Uploads/Thumbnails");
            string savedFileName = Path.Combine(filePathOriginal, filename);
            //image.SaveAs(savedFileName);

            //Read image back from file and create thumbnail from it
            var imageFile = Path.Combine(Server.MapPath("~/Content/Uploads/Originals"), filename);
            using (var srcImage = Image.FromFile(imageFile))
            using (var newImage = new Bitmap(100, 100))
            using (var graphics = Graphics.FromImage(newImage))
            using (var stream = new MemoryStream())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 100));
                newImage.Save(stream, ImageFormat.Png);
                var thumbNew = File(stream.ToArray(), "image/png");
            }
            return property.ImageContent;
            
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
                PropertyImages propertyImageDTO = context.PropertyImages.FirstOrDefault(y => y.UserId == Id);
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
                    ZipCode = propertyDTO.ZipCode, 
                    Rating = rating, //propertyDTO.Rating,
                    UtilitiesIncluded = propertyDTO.UtilitiesIncluded, 
                    Description = propertyDTO.Description,
                    ImageContent = propertyDTO.ImageContent,
                    ImagePath = propertyDTO.ImagePath
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