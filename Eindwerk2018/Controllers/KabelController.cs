using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;

namespace Eindwerk2018.Controllers
{
    public class KabelController : Controller
    {
        public ViewResult Index()
        {
            var kabels = GetKabels();
            return View (kabels);
        }

        public ActionResult Details(int id)
        {
            return View ("Details");
        }

        public ActionResult New()
        {
            var viewModel = new KabelFormViewModel();

            return View("KabelForm", viewModel);

        }

      

        // create is save geworden, zo kunnen we create en update in 1 view steken

        [HttpPost]
        public ActionResult Save(Kabel kabel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new KabelFormViewModel
                {
                   Kabel = kabel


                };
                return View("Index", viewModel);
            }

            if (kabel.Id == 0)
            {
                //schrijf naar database
            }

            else
            {
                // zoeken naar id en info overschrijven in DB 
                //UPDATE!!!!!!
            }

            //_context.SaveChanges();
            //bevestigen DB!!!!!!!!!!

            return RedirectToAction("Details", "Kabel", kabel);
        }

        public ActionResult Edit(int id)
        {

            

            var kabelTest = GetKabels();
            var kabelEdit = kabelTest.SingleOrDefault(c => c.Id == id);

            if (kabelTest == null)
                return HttpNotFound();

            var viewModel = new KabelFormViewModel()
            {
                Kabel = kabelEdit
                

            };
            return View("KabelForm", viewModel);
        }


        private IEnumerable<Kabel> GetKabels()
        {
            return new List<Kabel>
            {

                new Kabel {Id = 1, Naam = "Kabel1", Reference = "yyyyyyyyyyyy"},
                new Kabel {Id = 2, Naam = "Kabel2", Reference = "xxxxxxxxxxxx"},
                new Kabel {Id = 3, Naam = "Kabel3", Reference = "qqqqqqqqqqqq"}
            };

        }

       
    }
        
    }