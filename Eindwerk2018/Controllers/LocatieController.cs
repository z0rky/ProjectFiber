using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;
using System.Data.Entity;
using Eindwerk2018.Models.db;
using System.Net;

namespace Eindwerk2018.Controllers
{
    public class LocatieController : Controller
    {
        private Db_LocatieType dbLocatietypes = new Db_LocatieType();
        private Db_Locatie dbLocaties = new Db_Locatie();

        public List<LocatieType> locatielijst = new List<LocatieType>();
        //public List<Locatie> locatie = new List<Locatie>();
        //public List<Locatie> locatieFakeDataTest = new List<Locatie>();


        public void GetLocatieTypes()
        {
            locatielijst = dbLocatietypes.List(); 
        }

        public ViewResult Index()
        {   
            var locaties = GetLocaties();
            return View(locaties);
        } 

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var DetailsLocatie = dbLocaties.Get((int) id);

            return View("Details", DetailsLocatie);
        }

        public ActionResult New()
        {
            GetLocatieTypes();

            var viewModel = new LocatieFormViewModel()
            {
                Locatie = new Locatie { Id = 1 }, //fake id to make it 
                LocatieTypes = locatielijst
            };

            return View("LocatieForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (locatie.Id < 2) // 1 is fake id
            {
                locatie.Id = dbLocaties.Add(locatie);
            }
            else dbLocaties.Edit(locatie); //edit part

            return RedirectToAction("Details", "Locatie", new { Id = locatie.Id });
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

        [HttpPost]
        public JsonResult SearchLocaties(string Prefix)
        {
            List<Locatie> locaties = dbLocaties.SearchNaam(Prefix); //return too much for this
            //Converteren
            var list = from N in locaties select new { N.Id, N.LocatieNaam };
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
