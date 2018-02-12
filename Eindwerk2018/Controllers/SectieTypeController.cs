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
    public class SectieTypeController : Controller
    {
        private Db_SectieType dbSectieTypes = new Db_SectieType();

        // GET: OdfTypes
        public ActionResult Index()
        {
            var viewModel = dbSectieTypes.List(); //load lijst
            return View(viewModel);
        }

        // GET: OdfTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // GET: OdfTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OdfTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                //add to database
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
            //OdfType odfType = odftypelijst.Find(x => x.Id.Equals(id));
            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // POST: OdfTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
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

            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
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