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
    public class FoidController : Controller
    {
        private Db_Foid dbFoid = new Db_Foid();
        private Db_User dbUser = new Db_User();
        private Db_Odf dbOdf = new Db_Odf();


        public ActionResult Index()
        {
            var viewModel = dbFoid.List();
            return View (viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();

            //should order foid.Fibers;
            //and also add subkabels
            foid.Secties = OrderSecties(foid.Secties,foid.StartOdfId,foid.EndOdfId);

            return View (foid);
        }

        public ActionResult Create()
        {
            //users ophalen
            var viewModel = new NieuweFoidViewModel() { Users = dbUser.List() };

            return View(viewModel);
        } 

        [HttpPost]
        public ActionResult Create([Bind(Include = "Foid")] NieuweFoidViewModel newFoid)
        {
            if (ModelState.IsValid)
            {
                //add creation date
                newFoid.Foid.CreatieDatum = DateTime.Today;
                newFoid.Foid.LastStatusDate = DateTime.Today;
                //hardcode status as new
                newFoid.Foid.Status = 0;

                int newId = dbFoid.Add(newFoid.Foid);
                return RedirectToAction("Edit", "Foid", new { Id = newId });
            }
            //moet de lijst opnieuw aanmaken
            newFoid.Users = dbUser.List();
            return View(newFoid);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //statussen aanmaken
            var listStatuses = new List<Status>
            {  //name wordt in NameEn gezet, maar eigelijk is het al de juiste vertaling
                    new Status{ NameEn = Eindwerk2018.Resources.Resource.StatusNew , Id = 0 },
                    new Status{ NameEn = Eindwerk2018.Resources.Resource.StatusReserved , Id = 1 },
                    new Status{ NameEn = Eindwerk2018.Resources.Resource.StatusAccept , Id = 2 },
                    new Status{ NameEn = Eindwerk2018.Resources.Resource.StatusInService , Id = 3 },
                    new Status{ NameEn = Eindwerk2018.Resources.Resource.StatusRemoved , Id = 9 }
            };

            //foid ophalen
            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();

            //users ophalen
            var viewModel = new NieuweFoidViewModel() { Foid = foid,OldStatus=foid.Status, Users = dbUser.List(), Statuses = listStatuses };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Foid,OldStatus")] NieuweFoidViewModel newFoid)
        {
            if (ModelState.IsValid)
            {
                //if status is changed, update date; hoe weten we wat de vorige status was? -> added field OldStatus
                if(newFoid.OldStatus != newFoid.Foid.Status) newFoid.Foid.LastStatusDate = DateTime.Today;
                //toevoegen
                dbFoid.Edit(newFoid.Foid);
                return RedirectToAction("Details", "Foid", newFoid.Foid.Id);
            }

            return View(newFoid);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();
            return View(foid);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction ("Index");
            }
            catch
            {
                return View ();
            }
        }

        public ActionResult EditSections(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //foid ophalen
            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();

            //fiber omzetten naar Odfs en Secties
            List<Odf> startOdfs = new List<Odf>();
            List<Odf> endOdfs = new List<Odf>();
            List<Sectie> secties = foid.Secties;
            //odf start end should still be swapped
            foreach (Sectie sectie in secties)
            {
                startOdfs.Add(new Odf { Id = sectie.OdfStartId, Name = sectie.OdfStartName });
                //end odf ook?
                endOdfs.Add(new Odf { Id = sectie.OdfEndId, Name = sectie.OdfEndName });
                //secties.Add(new Sectie { SectieNr = sectie.SectieNr, KabelId = sectie.KabelId, KabelName = sectie.KabelName }); //ID?
            }
            //nolonger needed
            /*foreach (FiberFoid fiber in foid.Fibers.Where(f=> f.FoidFibreNr==1)) //should only select the first fiber, all the other are doubles
            {
                startOdfs.Add(new Odf { Id = fiber.OdfStartId, Name = fiber.OdfStartName });
                //end odf ook?
                endOdfs.Add(new Odf { Id = fiber.OdfEndId, Name = fiber.OdfEndName });
                secties.Add(new Sectie { SectieNr = fiber.SectieNr, KabelId = fiber.KabelId, KabelName = fiber.KabelName}); //ID?
            }*/

            var viewModel = new AddSectieFoidViewModel() { Foid = foid, StartOdfs = startOdfs, EndOdfs = endOdfs, Secties = secties };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditSections([Bind(Include = "Foid,Secties,Newsecties")] AddSectieFoidViewModel sectieFoid)
        {
            //for now just assuming there are only new secties
            //not really checking anything

            //and fiber number ? first free? how many?
            //for now assuming 2 fibers
            int serieNr = 100;
            int foidFibreNr = 1;
            int fiberNr = 1; //lookup with ListFreeFibers

            foreach (int sectieId in sectieFoid.Newsecties)
            {
                List<Fiber> freeFiberList = dbFoid.ListFreeFibers(sectieId);
                foidFibreNr = 1;
                fiberNr = freeFiberList[0].FiberNr;

                dbFoid.AddSecties(new FiberFoid() { Foid = sectieFoid.Foid.Id, SectieNr = sectieId, FoidSerialNr = serieNr, FoidFibreNr = foidFibreNr, FiberNr = fiberNr });
                //for now 2 foidFibreNr
                foidFibreNr = 2;
                fiberNr = freeFiberList[1].FiberNr;

                dbFoid.AddSecties(new FiberFoid() { Foid = sectieFoid.Foid.Id, SectieNr = sectieId, FoidSerialNr = serieNr, FoidFibreNr = foidFibreNr, FiberNr = fiberNr });
                serieNr += 100;
            };

            return RedirectToAction("Details", "Foid", new { Id = sectieFoid.Foid.Id });
        }

        private List<Sectie> OrderSecties(List<Sectie> startList, int beginStartOdfId, int beginEndOdfId)
        {
            List<Sectie> returnFiberList = new List<Sectie>();

            int startOdfId = beginStartOdfId;
            //not good can be multiple fibers !!

            foreach (Sectie sectie in startList)
            {
                //set start odf from first = to second
                if (startOdfId == sectie.OdfStartId) startOdfId = sectie.OdfEndId; //is the start for the next one
                else
                {   //swap start and end (id and name)
                    startOdfId = sectie.OdfStartId; //is the start for the next one
                    String tmp = sectie.OdfStartName;
                    sectie.OdfStartId = sectie.OdfEndId;
                    sectie.OdfStartName = sectie.OdfEndName;
                    sectie.OdfEndId = startOdfId;
                    sectie.OdfEndName = tmp;
                }
                returnFiberList.Add(sectie); //add it

                //should order foid.Fibers, combination of foidfibers;
                //and also add subkabels
                //checked on SectieVirtual if true, get sub thing, and should also check it, reuse this script
                if (sectie.SectieVirtual == true)
                {
                    //ophalen route
                    //Door dezelfde functie jagen
                }
            }

            return returnFiberList;
        }
    }
}