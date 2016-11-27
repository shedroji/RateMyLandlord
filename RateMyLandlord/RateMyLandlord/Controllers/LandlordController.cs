using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class LandlordController : Controller
    {
        // GET: Landlord
        public ActionResult Index()
        {
            return View();
        }
    }
}