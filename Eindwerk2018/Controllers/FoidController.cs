using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;
using Eindwerk2018.Reports;

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
                return RedirectToAction("Details", "Foid", new { Id = newId });
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

                //Bij status new (0) en removed (9), zou fibers moeten leeg gemaakt worden
                if (newFoid.Foid.Status == 0 || newFoid.Foid.Status == 9) dbFoid.DeleteFibers(newFoid.Foid.Id);

                //Edit
                dbFoid.Edit(newFoid.Foid);
                return RedirectToAction("Details", "Foid", new { Id = newFoid.Foid.Id });
            }

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
            Foid foid = dbFoid.Get((int) newFoid.Foid.Id);
            if (foid == null) return HttpNotFound();

            //users ophalen
            var viewModel = new NieuweFoidViewModel() { Foid = foid, OldStatus = foid.Status, Users = dbUser.List(), Statuses = listStatuses };

            return View(viewModel);
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
            if(secties[0] != null) //test
            { 
                foreach (Sectie sectie in secties)
                {
                    startOdfs.Add(new Odf { Id = sectie.OdfStartId, Name = sectie.OdfStartName });
                    //end odf ook?
                    endOdfs.Add(new Odf { Id = sectie.OdfEndId, Name = sectie.OdfEndName });
                    //secties.Add(new Sectie { SectieNr = sectie.SectieNr, KabelId = sectie.KabelId, KabelName = sectie.KabelName }); //ID?
                }
            }

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
                //hier zouden we met strategies kunnen werken,
                //eerste vrije fiber, of zoveel moglijk dezlfde fiber in secties van dezelfde kabel, ...
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

        public ActionResult EditFibers(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Foid foid = dbFoid.Get((int)id);
            if (foid == null) return HttpNotFound();

            EditFiberSectieViewModel viewModel = new EditFiberSectieViewModel();
            
            //should order foid.Fibers;
            //and also add subkabels
            try
            {
                foid.Secties = OrderSecties(foid.Secties, foid.StartOdfId, foid.EndOdfId,11); //11 for no cwdm sub sections
                //get the nr of fibers used.
                viewModel.NrOfFibers = foid.Secties[0].Fibers.Count();
                

                foreach (Sectie sectie in foid.Secties)
                { //add the free drivers
                    sectie.ListFreeFibers = dbFoid.ListFreeFibers(sectie.Id);
                    //add current selected fibers
                    foreach(Fiber fiber in sectie.Fibers) sectie.ListFreeFibers.Add(fiber);
                    //and order them
                    sectie.ListFreeFibers = sectie.ListFreeFibers.OrderBy(F => F.FiberNr).ToList();
                }
            }
            catch (Exception e) { }

            if (viewModel.NrOfFibers == 0) viewModel.NrOfFibers = 2; //set default ot 2
            viewModel.OldNrOfFibers = viewModel.NrOfFibers;
            viewModel.Foid = foid;

            return View(viewModel);
        }

        [HttpPost]
        //public ActionResult EditFibers(FormCollection collection) //could work, but difficult
        public ActionResult EditFibers([Bind(Include = "Foid,NrOfFibers,OldNrOfFibers,Secties,SectieFiber")] EditFiberSectieViewModel viewModel)
        {
            if (viewModel.NrOfFibers == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //check if nr of fiber equials nr of fibers.
            if (viewModel.NrOfFibers == viewModel.OldNrOfFibers)
            {
                //based on viewModel.Secties and viewModel.SectieFibers
                //add to foid
                Foid updateFoid = new Foid { Id= viewModel.Foid.Id};
                updateFoid.Secties = new List<Sectie>();
                foreach (int sectieId in viewModel.Secties) updateFoid.Secties.Add(new Sectie { Id= sectieId, Fibers = new List<Fiber>() });

                int j = 0;
                int i = 1;
                foreach (int fiberId in viewModel.SectieFiber)
                {
                    updateFoid.Secties[j].Fibers.Add(new Fiber { FiberNr = fiberId });
                    if( i % viewModel.NrOfFibers == 0) j++;
                    i++;
                }

                //update only fibers
                dbFoid.UpdateFibers(updateFoid);

                return RedirectToAction("Details", "Foid", new { Id = viewModel.Foid.Id });
            }
            else
            {
                //update secties with new nr of fibers
                try
                { //renew everything
                    Foid foid = dbFoid.Get((int)viewModel.Foid.Id);
                    viewModel.Foid = foid;
                    viewModel.Foid.Secties = OrderSecties(foid.Secties, foid.StartOdfId, foid.EndOdfId, 11);
                }
                catch (Exception e) { }

                //so remove or add fibers in for each section
                foreach (Sectie sectie in viewModel.Foid.Secties)
                {
                    //add free fibers
                    sectie.ListFreeFibers = dbFoid.ListFreeFibers(sectie.Id);

                    //add or delete ?
                    if (viewModel.NrOfFibers < viewModel.OldNrOfFibers)
                    {   //add
                        //int nrFibTemp = sectie.Fibers.Count(); //cant be set in the for, because it wil change when we remove
                        //for (int i = 0; i < nrFibTemp; i++)
                        //    if ((i + 1) > viewModel.NrOfFibers) sectie.Fibers.RemoveAt(i);
                        //beter :-)
                        while(viewModel.NrOfFibers < sectie.Fibers.Count()) sectie.Fibers.RemoveAt(sectie.Fibers.Count()-1);
                    }
                    else //groter -> add fibers
                    {
                        int j = 1;
                        for (int i = viewModel.OldNrOfFibers; i < viewModel.NrOfFibers; i++)
                        {
                            try
                            {
                                sectie.Fibers.Add(new Fiber { FiberNr = sectie.ListFreeFibers[j].FiberNr }); //should be the first next free fiber -> j 
                            }
                            catch (Exception e) { }

                            j++;
                        }
                    }

                    //add own
                    foreach (Fiber fiber in sectie.Fibers) if(fiber != null && fiber.FiberNr != 0) sectie.ListFreeFibers.Add(fiber);
                    //order
                    sectie.ListFreeFibers = sectie.ListFreeFibers.OrderBy(F => F.FiberNr).ToList(); //error
                }
            }

            //old gelijk zetten voor nieuwe pagina
            viewModel.OldNrOfFibers = viewModel.NrOfFibers;
            //OldNrOfFibers does not get to the page, Why ??
            //we can't update? -> Nu gedaan met jquery
            return View(viewModel);
        }


        private List<Sectie> OrderSecties(List<Sectie> startList, int beginStartOdfId, int beginEndOdfId, int level=0)
        {
            List<Sectie> returnFiberList = new List<Sectie>();

            if (startList == null) return returnFiberList; //safty check
            if (startList[0] == null) return returnFiberList;

            int startOdfId = beginStartOdfId;

            foreach (Sectie sectie in startList)
            {
                sectie.Level = level;
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
                if (sectie.SectieVirtual == true && level <10) // limit the amount of re-use of the function
                {
                    //eerst ophalen van de FOID van virtuele sectie
                    //uit de naam ?
                    String sectieFoid = sectie.KabelName.Substring(sectie.KabelName.IndexOf('.')+1);
                    //ophalen route
                    //Door dezelfde functie jagen
                    List<Sectie> tmpList = OrderSecties(dbFoid.ListSections(Convert.ToInt32(sectieFoid)), sectie.OdfStartId, sectie.OdfEndId, level + 1);
                    //geen subsectie -> just add another sectie
                    foreach (Sectie subSectie in tmpList) returnFiberList.Add(subSectie);
                }
            }

            return returnFiberList;
        }

        //for pdf
        public ActionResult ReportSectie(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Foid foid2 = dbFoid.Get((int) id);

            BezettingFoidModel bezettingFoidModel = new BezettingFoidModel();
            bezettingFoidModel.Foid = foid2;
            bezettingFoidModel.Secties = OrderSecties(foid2.Secties, foid2.StartOdfId, foid2.EndOdfId);

            BezettingFoidPdfReport bezettingFoidPdfReport = new BezettingFoidPdfReport();
            byte[] abytes2 = bezettingFoidPdfReport.PrepareReport(bezettingFoidModel);
            return File(abytes2, "application/pdf");
        }
    }
}