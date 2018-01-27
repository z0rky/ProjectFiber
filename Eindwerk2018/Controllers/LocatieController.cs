using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;
using System.Data.Entity;
using Eindwerk2018.Models.db;

namespace Eindwerk2018.Controllers
{
    public class LocatieController : Controller
    {
        private Db_LocatieType dbLocatieypes = new Db_LocatieType();
        private Db_Locatie dbLocaties = new Db_Locatie();

        public List<LocatieType> locatielijst = new List<LocatieType>();
        public List<Locatie> locatie = new List<Locatie>();
        public List<Locatie> locatieFakeDataTest = new List<Locatie>();


        public void FakeData()
        {
            locatielijst = dbLocatieypes.List(); //mag direkt worden opgeroepen
            //locatielijst.Add(new LocatieType(1, "brug", "bridge", "open", "ouvert"));
            //locatielijst.Add(new LocatieType(2, "caravan", "caravan", "gesloten", "fermee"));
            //locatielijst.Add(new LocatieType(3, "berg", "mont", "hoog", "haute"));
        }




        public ViewResult Index()
        {   
            //var locaties = _context.customers.Include(m => m.LocatieTypes )>ToList();
            //include voor andere models die we nodig hebben -> locatietypes
            var locaties = GetLocaties();
            return View(locaties);


        } 

        public ActionResult Details(int id)
        {
           //via API, werkt nu nog niet

            return View ("Details" );
        }

        public ActionResult New()
        {
            FakeData();

            var viewModel = new LocatieFormViewModel()
            {
                LocatieTypes = locatielijst

            };

            return View("LocatieForm", viewModel);
        }

        // create is save geworden, zo kunnen we create en update in 1 view steken

        [HttpPost]
        public ActionResult Save (Locatie locatie)
        {
            if (!ModelState.IsValid)
            {
                FakeData();
                var viewModel = new LocatieFormViewModel
                {
                    Locatie = locatie,
                    LocatieTypes = locatielijst

                };
                return View("Index", viewModel);
            }
            if (locatie.Id == 0)
            {
                //schrijf naar database
            }

            else
            {
                // zoeken naar id en info overschrijven in DB 
                //UPDATE!!!!!!
            }

            //_context.SaveChanges();
            //bevestigen DB!!!!!!!!!!

             return RedirectToAction("Details", "Locatie", locatie);
        }



        private IEnumerable<Locatie> GetLocaties()
        {
            return dbLocaties.List();
            //return new List<Locatie>
            //{
            //    new Locatie {Id=1, LocatieNaam = "Leuven station",GpsLong = 20,GpsLat = 20,LocatieInfrabel = true,LocatieTypeId = 2},
            //    new Locatie {Id=2, LocatieNaam = "Gent station",GpsLong = 10,GpsLat = 10,LocatieInfrabel = true,LocatieTypeId = 1},
            //    new Locatie {Id=3, LocatieNaam = "Brugge station",GpsLong = 30,GpsLat = 30,LocatieInfrabel = true,LocatieTypeId = 3}
            //};
        }

        


        public ActionResult Edit(int id)
        {
            
            FakeData();
            
            var locatieTest = GetLocaties();
            var locatieEdit = locatieTest.SingleOrDefault(c => c.Id == id);
             
            if (locatieTest == null)
                return HttpNotFound();

            var viewModel = new LocatieFormViewModel
            {
                Locatie = locatieEdit,
                LocatieTypes = locatielijst

            };
            return View("LocatieForm", viewModel);
        }
    }


}
