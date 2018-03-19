using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;

namespace Eindwerk2018.Controllers
{
    public class SearchKabelController : Controller
    {   
        private Db_Kabel dbKabels = new Db_Kabel();
        public List<Kabel> KabelsZoek = new List<Kabel>();

       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchReference()
        {
            return View("SearchKabelReference");

        }



        [HttpPost]
        public ActionResult SearchKabel(Kabel kabel)
        {
            if (kabel.Naam != null)
            {
            
                    KabelsZoek = dbKabels.Search(kabel.Naam).ToList();
                    var kabelZoekViewModel = new SearchKabelResultViewModel
                    {
                        GezochteKabels = KabelsZoek
                    };
                    return View("IndexSearchResult", kabelZoekViewModel);
              
                   
            }

            if (kabel.Reference != null)
            {
               
                  KabelsZoek = dbKabels.Search(kabel.Reference).ToList();
                    var kabelZoekViewModel = new SearchKabelResultViewModel
                    {
                        GezochteKabels = KabelsZoek
                    };
                    return View("IndexSearchResult", kabelZoekViewModel);
              
            }


            return View("Index");
        }

        public ActionResult Details(int id)
        {

            var DetailsKabel = dbKabels.Get(id);

            return View("~/Views/Kabel/Details.cshtml", DetailsKabel);
        }
    }
}