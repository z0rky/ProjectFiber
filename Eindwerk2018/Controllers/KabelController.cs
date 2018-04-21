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
        private Db_KabelType dbKabelstype = new Db_KabelType();
        private Db_Company dbCompanies = new Db_Company();
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

        public ActionResult Create()
        {
            //Types ophalen
            var viewModel = new NieuweKabelViewModel() { Kabel = new Kabel{ OwnerId=1 }, KabelTypes = dbKabelstype.List(), Companies = dbCompanies.List() };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(NieuweKabelViewModel kabelView )
        {
            if (ModelState.IsValid)
            {
                kabelView.Kabel.CreatieDatum = DateTime.Now;
                int newId = dbKabels.Add(kabelView.Kabel);

                if (newId != 0) return RedirectToAction("Edit", "Kabel", new { Id = newId });
            }

            var viewModel = new NieuweKabelViewModel
            {
                Kabel = kabelView.Kabel,
                KabelTypes = dbKabelstype.List(),
                Companies = dbCompanies.List()
            };
            return View("Create", viewModel);
        }
        
        public ActionResult Edit(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Kabel kabel = dbKabels.Get((int)id);
            if (kabel == null) return HttpNotFound();

            var viewModel = new NieuweKabelViewModel
            {
                Kabel = kabel,
                KabelTypes = dbKabelstype.List(),
                Companies = dbCompanies.List()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Kabel")] NieuweKabelViewModel kabelView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbKabels.Edit(kabelView.Kabel);
                    return RedirectToAction("Details", "Kabel", new { Id = kabelView.Kabel.Id });
                }
                catch { }
            }

            var viewModel = new NieuweKabelViewModel
            {
                Kabel = kabelView.Kabel,
                KabelTypes = dbKabelstype.List(),
                Companies = dbCompanies.List()
            };

            return View(viewModel);
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