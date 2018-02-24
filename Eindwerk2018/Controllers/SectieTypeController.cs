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

        // GET
        public ActionResult Index()
        {
            var viewModel = dbSectieTypes.List(); //load lijst
            return View(viewModel);
        }

        // GET: SectieTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // GET: SectieTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SectieTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //dbSectieTypes.Add();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SectieTypes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // POST: SectieTypes/Edit/5
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

        // GET: SectieTypes/Delete/5
        public ActionResult Delete(int id)
        {
            //FakeData(); //load data

            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // POST: SectieTypes/Delete/5
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