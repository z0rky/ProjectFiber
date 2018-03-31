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
        private Db_Foid dbFoid = new Db_Foid();
        public List<Foid> FoidZoek = new List<Foid>();


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchFoid(String SearchString)
        {
            if (ModelState.IsValid)
            {
                //test of het FOid nr is, or name-search
                if(int.TryParse(SearchString, out int n)) FoidZoek = dbFoid.SearchOnId(SearchString);
                else FoidZoek = dbFoid.Search(SearchString);

                //return View("Index","Foid", FoidZoek); //kan het niet vinden
                //return RedirectToAction("Index", "Foid", FoidZoek); //niet juist, herlaad de lijst
                return View("../Foid/Index", FoidZoek);
            }

            return View();
        }

    }
}