using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eindwerk2018.Models;
using Microsoft.Ajax.Utilities;

namespace Eindwerk2018.Controllers.Api
{
    public class LocatiesController : ApiController
    {   
        //constructor voor connectie met DB
        public LocatiesController()
        {
            //_context = new ApplicationDbContext();
        }

        //GET /api/locaties vanuit view

        public IEnumerable<Locatie> GetLocaties()
        {
            //return -context.Locaties.ToList();
            return new List<Locatie>
            {
                new Locatie
                {
                    Id = 1,
                    LocatieNaam = "Leuven station",
                    GpsLong = 20,
                    GpsLat = 20,
                    LocatieInfrabel = true,
                    LocatieTypeId = 2
                },
                new Locatie
                {
                    Id = 2,
                    LocatieNaam = "Gent station",
                    GpsLong = 10,
                    GpsLat = 10,
                    LocatieInfrabel = true,
                    LocatieTypeId = 1
                },
                new Locatie
                {
                    Id = 3,
                    LocatieNaam = "Brugge station",
                    GpsLong = 30,
                    GpsLat = 30,
                    LocatieInfrabel = true,
                    LocatieTypeId = 3
                }
            };

           
        }

        //GET  /api/locatie/1

        //public Locatie GetLocatie(int id)
        //{
            // var locatie = -context.Locaties.SingleOrDefault(c => c.Id == id);

            //if (Locatie == null)
            // throw new HttpResponseException(HttpStatusCode.NotFound);
                
            // return locatie;
        //}

        // POST  /api/locaties
        //[HttpPost]
        //public Locatie CreateLocatie(Locatie locatie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //        _context.Locaties.Add(locatie);
        //        _context.SaveChanges();

        //        return locatie;

        //    }
        //}

        // PUT api/locaties/1

        
       //[HttpPut]
        //public void UpdateLocatie(int id, Locatie locatie)
        //{
        //    if (!ModelState.IsValid)
           
        //            throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var locatieInDb = _context.Locatie.SingleOrDefault(c => c.Id == id);

        //    if (locatieInDb == null)

        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    locatieInDb.Name.......schrijven naar db
           
        //_context.SaveChanges();


        //}
    }
}
