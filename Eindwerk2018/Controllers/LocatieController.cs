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

        public List<LocatieType> locatielijst = new List<LocatieType>();
        public List<Locatie> locatie = new List<Locatie>();
        public List<Locatie> locatieFakeDataTest = new List<Locatie>();


        public void GetLocatieTypes()
        {
            locatielijst = dbLocatietypes.List(); 
        }




        public ViewResult Index()
        {   
            var locaties = GetLocaties();
            return View(locaties);


        } 

        public ActionResult Details(int id)
        {


            var DetailsLocatie = dbLocaties.Get(id);

            return View("Details", DetailsLocatie);
        }

        public ActionResult New()
        {
            GetLocatieTypes();

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
                GetLocatieTypes();
                var viewModel = new LocatieFormViewModel
                {
                    Locatie = locatie,
                    LocatieTypes = locatielijst

                };
                return View("LocatieForm", viewModel);
            }
            if (locatie.Id == 0)
            {
                dbLocaties.Add(locatie);
            }

            else
            {
                //
            }

            //_context.SaveChanges();
            //bevestigen DB!!!!!!!!!!

             return RedirectToAction("Details", "Locatie", locatie);
        }



        private IEnumerable<Locatie> GetLocaties()
        {
            return dbLocaties.List();
           
        }

        




        public ActionResult Edit(int id)
        {

           GetLocatieTypes();
            

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

        //public ActionResult Delete(int id)
        //{
        //    dbLocaties.Get(id);
        //}
    }


}
