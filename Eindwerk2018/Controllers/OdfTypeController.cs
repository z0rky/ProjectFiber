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
    public class OdfTypeController : Controller
    {
        private Db_OdfType dbOdfTypes = new Db_OdfType();

        // GET: OdfTypes
        public ActionResult Index()
        {
            var viewModel = dbOdfTypes.List(); //load lijst
            return View(viewModel);
        }

        // GET: OdfTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            OdfType odfType = dbOdfTypes.Get((int)id);
            if (odfType == null) return HttpNotFound();

            return View(odfType);
        }

        // GET: OdfTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OdfTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OdfTypeName,OdfTypeDescription")] OdfType odfType)
        {
            try
            {
                dbOdfTypes.Add(odfType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OdfTypes/Edit/5
        public ActionResult Edit(int id)
        {
            //FakeData(); //load data

            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            OdfType odfType = dbOdfTypes.Get((int)id);
            if (odfType == null) return HttpNotFound();

            return View(odfType);
        }

        // POST: OdfTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Description")] OdfType odfType)
        {
            try
            {
                dbOdfTypes.Edit(odfType);

                return RedirectToAction("Details", "OdfType", new { Id = odfType.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: OdfTypes/Delete/5
        public ActionResult Delete(int id)
        {
            //FakeData(); //load data

            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            OdfType odfType = dbOdfTypes.Get((int)id);
            if (odfType == null) return HttpNotFound();

            return View(odfType);
        }

        // POST: OdfTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}