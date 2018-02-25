using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
            try
            {
                // TODO: Add insert logic here
                dbLocatieTypes.Add(locatieType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
            try
            {
                dbLocatieTypes.Edit(locatieType);
                return RedirectToAction("Details", "LocatieType", new { Id = locatieType.Id });
            }
            catch
            {
                return View();
            }
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
    }
}
