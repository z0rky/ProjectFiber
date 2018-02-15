using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;

namespace Eindwerk2018.Controllers
{
    public class CompanyController : Controller
    {
        private Db_Company dbCompany = new Db_Company();

        // GET: Companies
        public ActionResult Index()
        {
            var viewModel = dbCompany.List();
            return View(viewModel);
        }

        // GET: Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Odf odf = odfLijst.Find(x => x.Id.Equals(id));
            Company company= dbCompany.Get((int)id);
            if (company == null) return HttpNotFound();

            return View(company);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                dbCompany.Add(company);
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = dbCompany.Get((int)id);

            if (company == null) return HttpNotFound();
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                dbCompany.Edit(company);
                return RedirectToAction("Company", "Details", company.Id);
            }

            return View(company);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = dbCompany.Get((int)id);
            if (company == null) return HttpNotFound();
            return View(company);
        }

        // POST: Company/Delete/5
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
