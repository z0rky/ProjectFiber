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
    public class SectieController : Controller
    {
        private Db_Sectie dbSectie = new Db_Sectie();

        // GET: Sectie
        public ActionResult Index(int? kabelId)
        {
            List < Sectie > secties = new List<Sectie>();
            if (kabelId == null) secties=dbSectie.List();
            else secties = dbSectie.Search((int)kabelId);

            return View(secties);
        }

        // GET: Sectie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Sectie sectie = dbSectie.Get((int)id);
            if (sectie == null) return HttpNotFound();

            return View(sectie);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sectie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectieTypeId,SectieKabelId,SectieOdfStartId,SectieOdfEndId,SectieLength")] Sectie sectie)
        {
            if (ModelState.IsValid)
            {
                dbSectie.Add(sectie);
                return RedirectToAction("Index");
                //return RedirectToAction("Sectie", "Details", sectie.Id); //should let add return id
            }

            return View(sectie);
        }

        // GET: user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Sectie sectie = dbSectie.Get((int)id);

            if (sectie == null) return HttpNotFound();
            return View(sectie);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SectieTypeId,SectieKabelId,SectieOdfStartId,SectieOdfEndId,SectieLength")] Sectie sectie)
        {
            if (ModelState.IsValid)
            {
                dbSectie.Edit(sectie);
                return RedirectToAction("Details", "Sectie", new { Id = sectie.Id });
            }

            return View(sectie);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Sectie sectie = dbSectie.Get((int)id);
            if (sectie == null) return HttpNotFound();
            return View(sectie);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //get info
            return RedirectToAction("Index");
        }
    }
}