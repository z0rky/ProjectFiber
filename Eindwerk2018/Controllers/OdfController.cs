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
using Eindwerk2018.ViewModels;

namespace Eindwerk2018.Controllers
{
    public class OdfController : Controller
    {
        private Db_Odf dbOdfs = new Db_Odf();
        private Db_OdfType dbOdfTypes = new Db_OdfType();

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
            //Types ophalen
            var viewModel = new NieuweOdfViewModel() { OdfTypes = dbOdfTypes.List() };

            return View(viewModel);
        }

        // POST: Odfs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocatieName,LocatieId,OdfTypeId,OdfName")] NieuweOdfViewModel odfModel)
        {
            if (ModelState.IsValid)
            {
                int newId = dbOdfs.Add(new Odf {
                        Name = odfModel.OdfName,
                        Location =new Locatie { Id= odfModel.LocatieId,LocatieNaam= odfModel.LocatieName},
                        OdfType = new OdfType { Id=odfModel.OdfTypeId}
                    });
                return RedirectToAction("Edit", "Odf", new { Id = newId });
            }

            //add list
            odfModel.OdfTypes = dbOdfTypes.List();

            return View(odfModel);
        }

        // GET: Odfs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Odf odf = dbOdfs.Get((int)id);
            if (odf == null) return HttpNotFound();
            //convert it to NieuweOdfViewModel
            NieuweOdfViewModel nieuweOdfView = new NieuweOdfViewModel {
                            Id =odf.Id,
                            LocatieName =odf.Location.LocatieNaam,
                            LocatieId = odf.Location.Id,
                            OdfTypeId = odf.OdfType.Id,
                            OdfName = odf.Name
                        };
            nieuweOdfView.OdfTypes = dbOdfTypes.List();

            return View(nieuweOdfView);
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
                return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult SearchOdfs(string Prefix)
        {
            List<Odf> odfs = dbOdfs.Search(Prefix); //return too much for this
            //Converteren
            var list = from N in odfs select new { N.Id, N.Name };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchConnectedOdf(int? odfId) //Search for connected Odf's of odfId (via section)
        {
            if(odfId != null && odfId !=0)
            { 
                List<Odf> odfs = dbOdfs.Search((int)odfId); //return too much for this
                //Converteren
                var list = from N in odfs select new { N.Id, N.Name };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
