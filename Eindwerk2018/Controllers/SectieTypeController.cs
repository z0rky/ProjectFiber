using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Resources;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;


namespace Eindwerk2018.Controllers
{
    public class SectieTypeController : Controller
    {
        private Db_SectieType dbSectieTypes = new Db_SectieType();

        // GET
        public ActionResult Index()
        {
            var viewModel = dbSectieTypes.List(); //load lijst
            return View(viewModel);
        }

        // GET: SectieTypes/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            sectieType.Fibers = dbSectieTypes.GetFibers(id);

            return View(sectieType);
        }

        // GET: SectieTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SectieTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Naam,Beschrijving,Virtueel")]SectieType sectieType)
        {
            try
            {
                if (dbSectieTypes.CheckName(sectieType.Naam)) ModelState.AddModelError("Naam", Resource.ErrorNameUnique);
                else
                {
                    int newId = dbSectieTypes.Add(sectieType);
                    if(newId>0) return RedirectToAction("Edit", "SectieType", new { Id = newId });
                }
            }
            catch { }
            return View();
        }

        // GET: SectieTypes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // POST: SectieTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam,Beschrijving,Virtueel")] SectieType sectieType)
        {
            try
            {
                if (dbSectieTypes.CheckName(sectieType.Naam,sectieType.Id)) ModelState.AddModelError("Naam", Resource.ErrorNameUnique);
                else
                {
                    dbSectieTypes.Edit(sectieType);
                    return RedirectToAction("Details", "SectieType", new { Id = sectieType.Id });
                }
            }
            catch { }
            return View();
        }

        // GET: SectieTypes/Delete/5
        public ActionResult Delete(int id)
        {
            //FakeData(); //load data

            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SectieType sectieType = dbSectieTypes.Get((int)id);
            if (sectieType == null) return HttpNotFound();

            return View(sectieType);
        }

        // POST: SectieTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {  // TODO: Add delete logic here
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /*Search*/
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string SearchString)
        {
            if (ModelState.IsValid)
            {
                var OdfList = dbSectieTypes.Search(SearchString);
                return View("Index", OdfList);
            }

            return View();
        }
    }
}