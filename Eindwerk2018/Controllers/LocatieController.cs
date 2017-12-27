using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;


namespace Eindwerk2018.Controllers
{
    public class LocatieController : Controller
    {
        public List<LocatieType> locatielijst = new List<LocatieType>();
        
        public void  FakeData()
        {    
            locatielijst.Add(new LocatieType(1, "brug", "bridge", "open", "ouvert"));
            locatielijst.Add(new LocatieType(2, "caravan", "caravan", "gesloten", "fermee"));
            locatielijst.Add(new LocatieType(3, "berg", "mont", "hoog", "haute"));
        }
        public ActionResult Index()
        {
            // database locatietypes toList


            
            FakeData();
            var viewModel = new NieuweLocatieViewModel
            {
                LocatieTypes = locatielijst

            };
            
              return View (viewModel);
        }

        public ActionResult Details(int id)
        {
            return View ();
        }



        [HttpPost]
        public ActionResult Create(Locatie locatie)
        {
            if (!ModelState.IsValid)
            {
                FakeData();
                var viewModel = new NieuweLocatieViewModel
                {
                    Locatie = locatie,
                    LocatieTypes = locatielijst

                };
                return View("Details", viewModel);
            }
            //schrijf naar database

            return RedirectToAction("Index", "Locatie");
        }
        
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

        public ActionResult Delete(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
    }
}