using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;

namespace Eindwerk2018.Controllers
{
    public class SearchFoidController : Controller
    {
        // private Db_Foid dbFoid = new Db_Foid();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchFoid(Foid foid)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SearchFoidViewModel
                {
                    Foid = foid
                };

                RedirectToAction("Index", "Foid", viewModel);
            }

            return View("Index");
        }

    }
}