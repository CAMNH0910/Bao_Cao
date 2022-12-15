using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T41.Controllers
{
    public class RatingZaloController : Controller
    {
        // GET: RatingZalo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RatingView()
        {
            return View();
        }
    }
}