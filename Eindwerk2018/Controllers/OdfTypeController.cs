using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;


namespace Eindwerk2018.Controllers
{
    public class OdfTypeController : Controller
    {
        public List<OdfType> odftypelijst = new List<OdfType>();

        public void FakeData()
        {
            odftypelijst.Add(new OdfType(1, "MOf","Mof"));
            odftypelijst.Add(new OdfType(2, "Las", "Las"));
            odftypelijst.Add(new OdfType(3, "connectie", "connectie"));
        }

        // GET: OdfTypes
        public ActionResult Index()
        {
            FakeData(); //load data

            var viewModel = odftypelijst;
            return View(viewModel);
        }

        // GET: OdfTypes/Details/5
        public ActionResult Details(int? id)
        {
            FakeData(); //load data

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            OdfType odfType = odftypelijst.Find(x=>x.Id.Equals(id));
            //OdfType odfType = odftypelijst.First();
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
            FakeData(); //load data

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            OdfType odfType = odftypelijst.Find(x => x.Id.Equals(id));
            if (odfType == null) return HttpNotFound();

            return View(odfType);
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
            FakeData(); //load data

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            OdfType odfType = odftypelijst.Find(x => x.Id.Equals(id));
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