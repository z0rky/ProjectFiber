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
    public class KabelController : Controller
    {
        private Db_Kabel dbKabels = new Db_Kabel();
        public List<Kabel> kabels = new List<Kabel>();
        

        public ActionResult Index()
        {
            var viewModel = dbKabels.List();
            return View (viewModel);
        }

        public ActionResult Details(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Kabel kabel = dbKabels.Get((int)id);
            if (kabel == null) return HttpNotFound();

            return View("Details",kabel);
        }

        public ActionResult New()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Save(Kabel kabel )
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NieuweKabelViewModel
                {
                    Kabel = kabel
                };
                return View("Create", viewModel);
            }
            else
            {   
                kabel.CreatieDatum = DateTime.Now;
                int newId = dbKabels.Add(kabel);

                return RedirectToAction("Edit", "Kabel", new { Id = newId });
            }
           
        }
        
        public ActionResult Edit(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Kabel kabel = dbKabels.Get((int)id);
            if (kabel == null) return HttpNotFound();

            return View("Edit", kabel);
        }

<<<<<<< HEAD
=======
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Naam,Reference")] Kabel kabel)
        {
            try
            {
                dbKabels.Edit(kabel);
                return RedirectToAction("Details", "Kabel", new { Id = kabel.Id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try {
        //        return RedirectToAction ("Index");
        //    } catch {
        //        return View ();
        //    }
        //}

>>>>>>> master
        [HttpPost]
        public JsonResult SearchKabel(string Prefix)
        {
            List<Kabel> kabels = dbKabels.Search(Prefix); //return too much for this
            //Converteren
            var list = from N in kabels select new { N.Id, N.Naam };
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}