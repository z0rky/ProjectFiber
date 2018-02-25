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
    public class KabelTypeController : Controller
    {
        private Db_KabelType dbKabelType = new Db_KabelType();

        // GET: KabelType
        public ActionResult Index()
        {
            var viewModel = dbKabelType.List();
            return View(viewModel);
        }

        // GET: KabelType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            KabelType kabelType = dbKabelType.Get((int)id);
            if (kabelType == null) return HttpNotFound();

            return View(kabelType);
        }

        // GET: KabelType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KabelType/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NameNL,NameFR")] KabelType kabelType)
        {
            try
            {
                dbKabelType.Add(kabelType);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KabelType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            KabelType kabelType = dbKabelType.Get((int)id);

            if (kabelType == null) return HttpNotFound();
            return View(kabelType);
        }

        // POST: KabelType/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,NameNL,NameFR")] KabelType kabelType)
        {
            try
            {
                dbKabelType.Edit(kabelType);
                return RedirectToAction("Details", "KabelType", new { Id = kabelType.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: KabelType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KabelType/Delete/5
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
