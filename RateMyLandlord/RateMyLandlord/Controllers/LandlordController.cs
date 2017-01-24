using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.Sql;

namespace RateMyLandlord.Controllers
{
    public class LandlordController : Controller
    {
        // GET: Landlords
        public ActionResult Index()
        {
            return View();
        }
    }
}