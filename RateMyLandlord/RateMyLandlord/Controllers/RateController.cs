using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyLandlord.Controllers
{
    public class RateController : Controller
    {
        // GET: Rate
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}