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
    public class ColorController : Controller
    {
        private Db_Color dbColor = new Db_Color();

        // GET: Color
        public ActionResult Index()
        {
            var viewModel = dbColor.List();
            return View(viewModel);
        }

        // GET: Color/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Color color = dbColor.Get((int)id);
            if (color == null) return HttpNotFound();

            return View(color);
        }

        // GET: Color/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Color/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Color color = dbColor.Get((int)id);
            if (color == null) return HttpNotFound();

            return View(color);
        }

        // POST: Color/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,NameEn,NameNl,NameFr")] Color color)
        {
            if (ModelState.IsValid)
            {
                dbColor.Edit(color);
                return RedirectToAction("Details", "Color", new { Id = color.Id });
            }

            return View(color);
        }

        // GET: Color/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Color color = dbColor.Get((int)id);
            if (color == null) return HttpNotFound();

            return View(color);
        }

        // POST: Color/Delete/5
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
