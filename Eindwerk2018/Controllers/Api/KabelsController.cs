using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eindwerk2018.Models;

namespace Eindwerk2018.Controllers.Api
{
    public class KabelsController : ApiController
    {
        //constructor

        public KabelsController()
        {
            //_context = new ApplicationDbContext();
        }

        public IEnumerable<Kabel> GetKabels()
        {
            //return -context.Kabels.ToList();
            return new List<Kabel>
            {
                new Kabel
                {
                    Id = 1,
                    Naam = "exvb 3g1.5",
                    Reference = "Telenet",
                    CreatieDatum = DateTime.Now

                },

                new Kabel
                {
                    Id = 2,
                    Naam = "exvb 3g1.5",
                    Reference = "Telenet",
                    CreatieDatum = DateTime.Now

                },

                new Kabel
                {
                    Id = 3,
                    Naam = "exvb 3g1.5",
                    Reference = "Telenet",
                    CreatieDatum = DateTime.Now

                },
            };
        }

        //GET  /api/kabel/1

        //public Locatie GetKabel(int id)
        //{
        // var Kabel = -context.Kabels.SingleOrDefault(c => c.Id == id);

        //if (Locatie == null)
        // throw new HttpResponseException(HttpStatusCode.NotFound);

        // return kabel;
        //}

        // POST  /api/kabels
        //[HttpPost]
        //public Kabel CreateLocatie(Kabel kabel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //        _context.Locaties.Add(kabel);
        //        _context.SaveChanges();

        //        return kabel;

        //    }
        //}

        // PUT api/kabels/1


        //[HttpPut]
        //public void UpdateKabel(int id, Kabel kabel)
        //{
        //    if (!ModelState.IsValid)

        //            throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var kabelInDb = _context.Kabel.SingleOrDefault(c => c.Id == id);

        //    if (kabelInDb == null)

        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    kabelInDb.Name.......schrijven naar db

        //_context.SaveChanges();


        //}
    }
}
