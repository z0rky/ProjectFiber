using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;

namespace Eindwerk2018.Controllers
{
    public class FoidController : Controller
    {
        private Db_Foid dbFoid = new Db_Foid();
        private Db_User dbUser = new Db_User();
        private Db_Odf dbOdf = new Db_Odf();


        public ActionResult Index()
        {
            var viewModel = dbFoid.List();
            return View (viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();

            return View (foid);
        }

        public ActionResult Create()
        {
            //users ophalen
            var viewModel = new NieuweFoidViewModel() { Users = dbUser.List() };

            return View(viewModel);
        } 

        [HttpPost]
        public ActionResult Create([Bind(Include = "Foid")] NieuweFoidViewModel newFoid)
        {
            if (ModelState.IsValid)
            {
                //add creation date
                newFoid.Foid.CreatieDatum = DateTime.Today;
                newFoid.Foid.LastStatusDate = DateTime.Today;
                //hardcode status as new
                newFoid.Foid.Status = 0;

                int newId = dbFoid.Add(newFoid.Foid);
                return RedirectToAction("Edit", "Foid", new { Id = newId });
            }
            //moet d elijst opnieuw aanmaken
            newFoid.Users = dbUser.List();
            return View(newFoid);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //users ophalen
            var viewModel = new NieuweFoidViewModel() { Foid = dbFoid.Get((int)id), Users = dbUser.List() };

            if (viewModel.Foid == null) return HttpNotFound();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Comments,LengthOtdr")] Foid foid)
        {
            if (ModelState.IsValid)
            {
                //if status is changed, update date; hoe weten we wat de vorige status was?
                //foid.LastStatusDate = new DateTime();
                dbFoid.Edit(foid);
                return RedirectToAction("Details", "Foid", foid.Id);
            }

            return View(foid);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();
            return View(foid);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction ("Index");
            }
            catch
            {
                return View ();
            }
        }
    }
}