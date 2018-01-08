using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;

namespace Eindwerk2018.Controllers
{
    public class FoidController : Controller
    {
        public ViewResult Index()
        {
            //NieuweFoidviewmodel ophalen
            var foids = GetFoids();
            return View(foids);

           
        }

        public ActionResult Details(int id)
        {
            return View ();
        }

        public ActionResult New()
        {


            var viewModel = new FoidFormViewModel();
            
            return View("FoidForm", viewModel);


        }

        [HttpPost]
        public ActionResult Save(Foid foid)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new FoidFormViewModel
                {
                    Foid = foid


                };
                return View("Index", viewModel);
            }
            return RedirectToAction("Index", "Foid");
        }
        
       



        private IEnumerable<Foid> GetFoids()
        {
            return new List<Foid>
            {
                new Foid
                {
                    Id=1,
                    Name = "foid1",
                    Status = true,
                    RequestorId = 3,
                    CreatieDatum = DateTime.Now

                },
                new Foid
                {
                    Id=2,
                    Name = "foid2",
                    Status = false,
                    RequestorId = 2,
                    CreatieDatum = DateTime.Now

                },
                new Foid
                {
                    Id=3,
                    Name = "foid3",
                    Status = false,
                    RequestorId = 1,
                    CreatieDatum = DateTime.Now

                }
            };


        }
    }
}