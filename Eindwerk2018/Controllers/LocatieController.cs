﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.ViewModels;
using System.Data.Entity;
using Eindwerk2018.Models.db;

namespace Eindwerk2018.Controllers
{
    public class LocatieController : Controller
    {
        private Db_LocatieType dbLocatieypes = new Db_LocatieType();
        private Db_Locatie dbLocaties = new Db_Locatie();

        public List<LocatieType> locatielijst = new List<LocatieType>();
        public List<Locatie> locatie = new List<Locatie>();
        public List<Locatie> locatieFakeDataTest = new List<Locatie>();


        public void FakeData()
        {
            locatielijst = dbLocatieypes.List(); 
        }




        public ViewResult Index()
        {   
            var locaties = GetLocaties();
            return View(locaties);


        } 

        public ActionResult Details(int id)
        {
           
            return View ("Details");
        }

        public ActionResult New()
        {
            FakeData();

            var viewModel = new LocatieFormViewModel()
            {
                LocatieTypes = locatielijst

            };

            return View("LocatieForm", viewModel);
        }

       

        [HttpPost]
        public ActionResult Save (Locatie locatie)
        {
            if (!ModelState.IsValid)
            {
                FakeData();
                var viewModel = new LocatieFormViewModel
                {
                    Locatie = locatie,
                    LocatieTypes = locatielijst

                };
                return View("Index", viewModel);
            }
            if (locatie.Id == 0)
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

             return RedirectToAction("Details", "Locatie", locatie);
        }



        private IEnumerable<Locatie> GetLocaties()
        {
            return dbLocaties.List();
           
        }

        




        public ActionResult Edit(int id)
        {
            
            FakeData();
            
            var locatieTest = GetLocaties();
            var locatieEdit = locatieTest.SingleOrDefault(c => c.Id == id);
             
            if (locatieTest == null)
                return HttpNotFound();

            var viewModel = new LocatieFormViewModel
            {
                Locatie = locatieEdit,
                LocatieTypes = locatielijst

            };
            return View("LocatieForm", viewModel);
        }
    }


}
