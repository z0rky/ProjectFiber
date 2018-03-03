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
            var viewModel = new NieuweFoidViewModel() { Users = dbUser.List() }; //not complete list

            return View(viewModel);
        } 

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Comments,LengthOtdr")] Foid foid)
        {
            if (ModelState.IsValid)
            {
                //add creation date
                foid.CreatieDatum = new DateTime();
                foid.LastStatusDate = new DateTime();
                //hardcode status as new
                foid.Status = 0;

                dbFoid.Add(foid);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Foid");
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Foid foid = dbFoid.Get((int)id);

            if (foid == null) return HttpNotFound();
            return View(foid);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Comments,LengthOtdr")] Foid foid)
        {
            if (ModelState.IsValid)
            {
                //if status is changed, update date; hoe weten we wat de vorige status was?
                //foid.LastStatusDate = new DateTime();
                dbFoid.Edit(foid);
                return RedirectToAction("Foid", "Details", foid.Id);
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