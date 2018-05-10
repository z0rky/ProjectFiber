using Eindwerk2018.Models;
using Eindwerk2018.Resources;
using Eindwerk2018.Models.db;
using System;
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
            if (ModelState.IsValid)
            {
                try
                {
                    if (dbKabelType.CheckName(kabelType.NameNL)) ModelState.AddModelError("NameNL", Resource.ErrorNameUnique);
                    else
                    {
                        int newId = dbKabelType.Add(kabelType);
                        return RedirectToAction("Edit", "KabelType", new { Id = newId });
                    }
                }
                catch { }
            }

            return View(kabelType);
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
            if (ModelState.IsValid)
            {
                try
                {
                    if (dbKabelType.CheckName(kabelType.NameNL,kabelType.Id)) ModelState.AddModelError("NameNL", Resource.ErrorNameUnique);
                    else
                    {
                        dbKabelType.Edit(kabelType);
                        return RedirectToAction("Details", "KabelType", new { Id = kabelType.Id });
                    }
                }
                catch { }
            }
            return View(kabelType);
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
