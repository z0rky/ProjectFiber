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

            return View(kabel);
        }

        public ActionResult Create()
        {
            return View ();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection )
        {
            //if (!ModelState.IsValid)
            //{
            //    var viewModel = new NieuweFoidViewModel
            //    {
            //        Foid = foid


            //    };
            //    return View("Index", viewModel);
            //}
            //return RedirectToAction("Index", "")
            return View("index");
        }
        
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

        public ActionResult Delete(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
    }
}