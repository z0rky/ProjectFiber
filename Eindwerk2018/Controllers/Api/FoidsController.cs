using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eindwerk2018.Models;

namespace Eindwerk2018.Controllers.Api
{
    public class FoidsController : ApiController
    {
        public FoidsController()
        {
            //_context = new ApplicationDbContext();
        }

        public IEnumerable<Foid> GetFoids()
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

            //GET  /api/foid/1

            //public Locatie GetFoid(int id)
            //{
            // var foid = -context.Locaties.SingleOrDefault(c => c.Id == id);

            //if (Locatie == null)
            // throw new HttpResponseException(HttpStatusCode.NotFound);

            // return foid;
            //}

            // POST  /api/foids
            //[HttpPost]
            //public Foid CreateFoid(Foid foid)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        throw new HttpResponseException(HttpStatusCode.BadRequest);

            //        _context.Foids.Add(foid);
            //        _context.SaveChanges();

            //        return foid;

            //    }
            //}

            // PUT api/locaties/1


            //[HttpPut]
            //public void UpdateFoid(int id, Foid foid)
            //{
            //    if (!ModelState.IsValid)

            //            throw new HttpResponseException(HttpStatusCode.BadRequest);

            //    var foidInDb = _context.Foid.SingleOrDefault(c => c.Id == id);

            //    if (foidInDb == null)

            //        throw new HttpResponseException(HttpStatusCode.NotFound);

            //    foidInDb.Name.......schrijven naar db

            //_context.SaveChanges();


            //}

        }
    }
}
