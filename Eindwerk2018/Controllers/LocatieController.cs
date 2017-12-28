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
            
              return View ("Index",viewModel);
        }

        public ActionResult Details(Locatie locatie)
        {
           
            return View ("Details", locatie);
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
                return View("Index", viewModel);
            }
            //schrijf naar database

            return RedirectToAction("Details", "Locatie", locatie);
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