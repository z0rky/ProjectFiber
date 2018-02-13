using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eindwerk2018.Controllers
{
    public class SearchKabelController : Controller
    {
        // GET: SearchKabel
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult SearchReference()
        {
            return View("SearchKabelReference");
        }
    }
}