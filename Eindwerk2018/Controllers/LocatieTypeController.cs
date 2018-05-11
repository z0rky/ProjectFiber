using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Eindwerk2018.Resources;


namespace Eindwerk2018.Controllers
{
    public class LocatieTypeController : Controller
    {
        private Db_LocatieType dbLocatieTypes = new Db_LocatieType();

        // GET: LocationType
        public ActionResult Index()
        {
            var viewModel = dbLocatieTypes.List(); //load lijst
            return View(viewModel);
        }

        // GET: LocationType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LocatieType locatieType = dbLocatieTypes.Get((int)id);
            if (locatieType == null) return HttpNotFound();

            return View(locatieType);
        }

        // GET: LocationType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NaamNL,NaamFR,DescNL,DescFR")] LocatieType locatieType)
        {
            if (!ModelState.IsValid) return View(locatieType);

            try
            {
                if (dbLocatieTypes.CheckName(locatieType.NaamNL)) ModelState.AddModelError("NaamNL", Resource.ErrorNameUnique);
                else
                {
                    int newId = dbLocatieTypes.Add(locatieType);
                    if(newId> 0) return RedirectToAction("Edit", "LocationType", new { Id = newId });
                }
            }
            catch { }

            return View(locatieType);
        }

        // GET: LocationType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LocatieType locatieType = dbLocatieTypes.Get((int)id);

            if (locatieType == null) return HttpNotFound();
            return View(locatieType);
        }

        // POST: LocationType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,NaamNL,NaamFR,DescNL,DescFR")] LocatieType locatieType)
        {
            if (!ModelState.IsValid) return View(locatieType);

            try
            {
                if (dbLocatieTypes.CheckName(locatieType.NaamNL, locatieType.Id)) ModelState.AddModelError("NaamNL", Resource.ErrorNameUnique);
                else
                {
                    dbLocatieTypes.Edit(locatieType);
                    return RedirectToAction("Details", "LocatieType", new { Id = locatieType.Id });
                }
            }
            catch { }

            return View(locatieType);
        }

        // GET: LocationType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*Search*/
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string SearchString)
        {
            if (ModelState.IsValid)
            {
                var OdfList = dbLocatieTypes.Search(SearchString);
                return View("Index", OdfList);
            }

            return View();
        }
    }
}
