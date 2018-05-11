using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Resources;
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
        public ActionResult Create([Bind(Include = "Name,Description")] OdfType odfType)
        {
            if(ModelState.IsValid)
            { 
                try
                {
                    if (dbOdfTypes.CheckName(odfType.Name)) ModelState.AddModelError("Name", Resource.ErrorNameUnique);
                    else
                    {
                        int newId = dbOdfTypes.Add(odfType);
                        return RedirectToAction("Edit", "OdfType", new { Id = newId });
                    }
                }
                catch { }
            }
            return View(odfType);
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
            if (!ModelState.IsValid) return View(odfType);

            try
            {
                if (dbOdfTypes.CheckName(odfType.Name,odfType.Id)) ModelState.AddModelError("Name", Resource.ErrorNameUnique);
                else
                {
                    dbOdfTypes.Edit(odfType);

                    return RedirectToAction("Details", "Odftype", new { Id = odfType.Id });
                }
            }
            catch { }
            return View(odfType);
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
                var OdfTypesList = dbOdfTypes.Search(SearchString);
                return View("Index", OdfTypesList);
            }

            return View();
        }
    }
}