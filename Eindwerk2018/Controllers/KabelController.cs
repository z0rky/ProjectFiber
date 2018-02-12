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
        public ActionResult Index()
        {   
           
            return View ();
        }

        public ActionResult Details(int id)
        {
            return View ();
        }

        public ActionResult Create()
        {
            return View ();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection )
        {
            //if (!ModelState.IsValid)
            //{
            //    var viewModel = new NieuweFoidViewModel
            //    {
            //        Foid = foid


            //    };
            //    return View("Index", viewModel);
            //}
            //return RedirectToAction("Index", "")
            return View("index");
        }
        
        public ActionResult Edit(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }

        public ActionResult Delete(int id)
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
    }
}