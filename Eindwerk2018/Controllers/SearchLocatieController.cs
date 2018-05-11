using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Eindwerk2018.Models;
using Eindwerk2018.Models.db;
using Eindwerk2018.ViewModels;


namespace Eindwerk2018.Controllers
{
    public class SearchLocatieController : Controller
    {

        private Db_Locatie dbLocaties = new Db_Locatie();
        public List<Locatie> LocatieZoek = new List<Locatie>();


        public ViewResult Index()
        {
            return View();
        }

        public ActionResult SearchLcode()
        {
            return View("Index");
        }

        public ActionResult SearchNaam()
        {
            return View("SearchLocatieNaam");
        }

        public ActionResult SearchGps()
        {
            return View("SearchLocatieGps");
        }

        public ActionResult SearchPostCode()
        {
            return View("SearchLocatiePostCode");
        }

        public ActionResult SearchPlaats()
        {
            return View("SearchLocatiePlaats");
        }

        [HttpPost]
        public ActionResult ZoekLocatie(Locatie locatie)
        {
            //naam OK
            if (locatie.LocatieNaam != null)
            {
                if (!ModelState.IsValid)
                {
                    LocatieZoek = dbLocaties.SearchNaam(locatie.LocatieNaam).ToList();
                    var locatieZoekViewModel = new SearchLocatieResultViewModel
                    {
                        GezochteLocaties = LocatieZoek
                    };
                    return View("IndexSearchResult", locatieZoekViewModel);
                }
                else
                {
                    var viewModel = new SearchLocatieViewModel
                    {
                        Locatie = locatie
                    };

                    return View("SearchLocatieNaam", viewModel);
                    
                }


            }
            
            // mogelijkheid om enkel op long of lat te zoeken??
            
            if ((locatie.GpsLat !=0) || (locatie.GpsLong != 0))
            {
                if (!ModelState.IsValid)
                {
                    LocatieZoek = dbLocaties.SearchGPS((double)locatie.GpsLat, (double)locatie.GpsLong).ToList();
                    var locatieZoekViewModel = new SearchLocatieResultViewModel
                    {
                        GezochteLocaties = LocatieZoek
                    };
                    return View("IndexSearchResult", locatieZoekViewModel);

                    
                }
                else
                {
                    var viewModel = new SearchLocatieViewModel
                    {
                        Locatie = locatie
                    };

                    return View("SearchLocatieGps", viewModel);
                }

            }
            

            // nog query maken !!!!!!!!
            if (locatie.PostCode != 0)
            {
                if (!ModelState.IsValid)
                {   
                    
                    LocatieZoek = dbLocaties.SearchPostCode((int)locatie.PostCode).ToList();
                    var locatieZoekViewModel = new SearchLocatieResultViewModel
                    {
                        GezochteLocaties = LocatieZoek
                    };
                    return View("IndexSearchResult", locatieZoekViewModel);
                }
                else
                {
                    var viewModel = new SearchLocatieViewModel
                    {
                        Locatie = locatie
                    };

                    return View("SearchLocatiePostCode", viewModel);
                }

            }

            // nog query maken !!!!!!!!!!!!
            if (locatie.Plaats != null)
            {
                if (!ModelState.IsValid)
                {

                    LocatieZoek = dbLocaties.SearchPlaats(locatie.Plaats).ToList();
                    var locatieZoekViewModel = new SearchLocatieResultViewModel
                    {
                        GezochteLocaties = LocatieZoek
                    };
                    return View("IndexSearchResult", locatieZoekViewModel);
                }
                else
                {
                    var viewModel = new SearchLocatieViewModel
                    {
                        Locatie = locatie
                    };

                    return View("SearchLocatiePlaats", viewModel);


                }

            }

            return View("Index");
        }

        private IEnumerable<Locatie> GezochteLocaties()
        {
            return dbLocaties.List();

        }

        public ActionResult Details(int id)
        {

            var DetailsLocatie = dbLocaties.Get(id);

            return View("~/Views/Locatie/Details.cshtml", DetailsLocatie);
        }
    }
}
