using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eindwerk2018.Controllers
{
    public class SectieController : Controller
    {
        // GET: Sectie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            throw new NotImplementedException();
        }
    }
}