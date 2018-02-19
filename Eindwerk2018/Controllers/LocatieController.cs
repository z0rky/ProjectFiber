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
        private Db_LocatieType dbLocatietypes = new Db_LocatieType();
        private Db_Locatie dbLocaties = new Db_Locatie();

        public  List<LocatieType> locatielijst = new List<LocatieType>();
        public List<Locatie> locatie = new List<Locatie>();
        public List<Locatie> locatieFakeDataTest = new List<Locatie>();


        public void FakeData()
        {
            locatielijst = dbLocatietypes.List(); //mag direkt worden opgeroepen
            //locatielijst.Add(new LocatieType(1, "brug", "bridge", "open", "ouvert"));
            //locatielijst.Add(new LocatieType(2, "caravan", "caravan", "gesloten", "fermee"));
            //locatielijst.Add(new LocatieType(3, "berg", "mont", "hoog", "haute"));
        }





        public ViewResult Index()
        {

            var locaties = GetLocaties();
            return View(locaties);


        } 

        public ActionResult Details(Locatie locatie)
        {
            return View ("Details", locatie);
        }

        public ActionResult New()
        {
            

            var viewModel = new LocatieFormViewModel()
            {   
                LocatieTypes = locatielijst

            };

            return View("LocatieForm", viewModel);
        }

       

        [HttpPost]
        public ActionResult Save (Locatie locatie)
        {
            if (!ModelState.IsValid)
            {
               
                var viewModel = new LocatieFormViewModel
                {
                    Locatie = locatie,
                    LocatieTypes = dbLocatietypes.List()

                };
                return View("Index", viewModel);
            }
            if (locatie.Id == 0)
            {
                dbLocaties.Add(locatie);
            }

            else
            {
               var locatieId =  locatie.Id;
               var locatieTest = GetLocaties();
               var locatieEdit = locatieTest.SingleOrDefault(c => c.Id == locatieId);


                locatie.LocatieNaam = locatieEdit.LocatieNaam;
                locatie.GpsLat = locatieEdit.GpsLat;
                locatie.GpsLong = locatieEdit.GpsLong;
                locatie.Lcode = locatieEdit.Lcode;
                locatie.LocatieBedrijf = locatieEdit.LocatieBedrijf;
                locatie.LocatieInfrabel = locatieEdit.LocatieInfrabel;
                locatie.LocatieTypeId = locatieEdit.LocatieTypeId;
                locatie.Plaats = locatieEdit.Plaats;
                locatie.PostCode = locatieEdit.PostCode;
                locatie.Straat = locatieEdit.Straat;
                locatie.HuisNr = locatieEdit.HuisNr;

                dbLocaties.Add(locatie);
                
                


            }



            //_context.SaveChanges();
            //bevestigen DB!!!!!!!!!!

            return RedirectToAction("Details", "Locatie", locatie);
        }



        private IEnumerable<Locatie> GetLocaties()
        {
            return dbLocaties.List();
           
        }

        private IEnumerable<LocatieType> GetLocatieTypes()
        {
            return dbLocatietypes.List();

        }


        //public ActionResult Edit(int id)
        //{

        //    FakeData();

        //    var locatieTest = GetLocaties();
        //    var locatieEdit = locatieTest.SingleOrDefault(c => c.Id == id);

        //    if (locatieTest == null)
        //        return HttpNotFound();

        //    var viewModel = new LocatieFormViewModel
        //    {
        //        Locatie = locatieEdit,
        //        LocatieTypes = locatielijst

        //    };
        //    return View("LocatieForm", viewModel);
        //}
    }


}
