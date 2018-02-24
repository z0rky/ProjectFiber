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
        public ActionResult SearchFoid(Foid foid)
        {
            if (!ModelState.IsValid)
            {
                FoidZoek = dbFoid.Search(foid.Name).ToList();
                var foidZoekViewModel = new SearchFoidResultViewModel
                {
                    GezochteFoids = FoidZoek
                };
                return View("IndexSearchResult",foidZoekViewModel);
            }

            var viewModel = new SearchFoidViewModel
            {
                Foid = foid
            };

            return View("Index", viewModel);
        }

    }
}