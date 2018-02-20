using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;

namespace Eindwerk2018.Controllers
{
    public class OdfController : Controller
    {
        private Db_Odf dbOdfs = new Db_Odf();

        // GET: Odfs
        public ActionResult Index()
        {
            var viewModel = dbOdfs.List();
            return View(viewModel);
        }

        // GET: Odfs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Odf odf = dbOdfs.Get((int)id);
            if (odf == null) return HttpNotFound();

            return View(odf);
        }

        // GET: Odfs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Odfs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Location_id,Type_id,Name")] Odf odf)
        {
            if (ModelState.IsValid)
            {
                dbOdfs.Add(odf);
                return RedirectToAction("Index");
            }

            return View(odf);
        }

        // GET: Odfs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Odf odf = dbOdfs.Get((int)id);

            if (odf == null) return HttpNotFound();
            return View(odf);
        }

        // POST: Odfs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location_id,Type_id,Name")] Odf odf)
        {
            if (ModelState.IsValid)
            {
                dbOdfs.Edit(odf);
                return RedirectToAction("Details", "Odf", new { Id = odf.Id });
            }
            return View(odf);
        }

        // GET: Odfs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Odf odf = dbOdfs.Get((int)id);
            if (odf == null) return HttpNotFound();
            return View(odf);
        }

        // POST: Odfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Odf odf = db.Odfs.Find(id);
            //db.Odfs.Remove(odf);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //delete
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
