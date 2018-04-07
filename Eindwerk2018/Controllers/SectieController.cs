using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Reports;

namespace Eindwerk2018.Controllers
{
    public class SectieController : Controller
    {
        private Db_Sectie dbSectie = new Db_Sectie();
        private Db_SectieType dbSectieType = new Db_SectieType();
        //BezettingVanDeVezelsModel bezettingVanDeVezelsModel; 

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


           //bezettingVanDeVezelsModel = new BezettingVanDeVezelsModel();
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Sectie sectie = dbSectie.Get((int)id);
            //bezettingVanDeVezelsModel.sectie = sectie;
            //bezettingVanDeVezelsModel.Fibers = sectie.Fibers;

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
                //also add all fibers from section_type_inf in fibers (is in the dbSectie.Add)

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
        { //wordt nog niet gedaan, en mag waarschinlijk ook niet
            if (ModelState.IsValid)
            {
                //dbSectie.Edit(sectie);
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
            if(odfId != null)
            { 
                List<Sectie> secties = dbSectie.SearchOdf((int) odfId); //return too much for this
                //Converteren
                var list = from N in secties select new { N.Id, N.KabelName, N.SectieNr, N.OdfStartId, N.OdfStartName, N.OdfEndId, N.OdfEndName };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public JsonResult LastSectionOfKabel(int? kabelId)
        {
            if (kabelId != null)
            {
                Sectie lastSectie = dbSectie.GetLastSection((int)kabelId); //return too much for this
                //Converteren
                if (lastSectie == null) lastSectie = new Sectie { SectieNr =0, SectionTypeId=0, SectionTypeName=null, OdfEndId=0, OdfEndName=null }; //empty aanmaken

                var list = new { lastSectie.SectieNr, lastSectie.SectionTypeId, lastSectie.SectionTypeName, lastSectie.OdfEndId, lastSectie.OdfEndName };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public JsonResult ReturnSectiesInfo(int? sectieId)
        {
            if (sectieId != null && sectieId != 0)
            {
                Sectie sectie = dbSectie.Get((int)sectieId);
                //Converteren
                var ajaxSectie = new { sectie.Id, sectie.SectieNr, sectie.KabelId, sectie.KabelName, sectie.OdfStartId, sectie.OdfStartName, sectie.OdfEndId, sectie.OdfEndName };
                return Json(ajaxSectie, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        public ActionResult ReportSectie(Sectie sectie )
        {
            int tempId = sectie.Id;
            Sectie sectie2 = dbSectie.Get(tempId);


            BezettingVanDeVezelsModel bezettingVanDeVezelsModel = new BezettingVanDeVezelsModel();
            bezettingVanDeVezelsModel.sectie = sectie2;
            bezettingVanDeVezelsModel.Fibers = sectie2.Fibers.ToList();

            BezettingVanDeVezelsPdfReport bezettingVanDeVezelsPdfReport = new BezettingVanDeVezelsPdfReport();
            byte[] abytes2 = bezettingVanDeVezelsPdfReport.PrepareReport(bezettingVanDeVezelsModel);
            return File(abytes2, "application/pdf");
        }

        public List<Sectie> GetSectie()
        {
            List<Sectie> sectieList = new List<Sectie>();
            //// get foids to print

            Sectie sectie = new Sectie();

            sectie.Id = 1;
            sectie.Lengte = 23;
            sectieList.Add(sectie);


            return sectieList;

        }
    }
}