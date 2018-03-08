using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;
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
        private Db_SectieType dbSectieType = new Db_SectieType();

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
            var viewModel = new NieuweSectieViewModel() { SectieTypes = dbSectieType.List() };

            return View(viewModel);
        }

        // POST: Sectie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sectie")] NieuweSectieViewModel newSectie)
        {
            if (ModelState.IsValid)
            {
                newSectie.Sectie.Active = true; //een nieuwe is altijd actief
                int newId = dbSectie.Add(newSectie.Sectie);

                //should also add all fibers
   
                return RedirectToAction("Details", "Sectie", new { Id = newId });
            }

            return View(newSectie);
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
                return RedirectToAction("Sectie", "Details", sectie.Id);
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

        [HttpPost]
        public JsonResult SearchSecties(int? odfId)
        {
            List<Sectie> secties = dbSectie.SearchOdf((int) odfId); //return too much for this
            //Converteren
            var list = from N in secties select new { N.Id, N.KabelName, N.SectieNr }; //hoe kablename en SectieNr samen voegen ?
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}