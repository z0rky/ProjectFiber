using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;
using Eindwerk2018.Reports;

namespace Eindwerk2018.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdf
        public ActionResult Report(Foid foid)
        {
            PdfReports pdfReports = new PdfReports();
            byte[] abytes = pdfReports.PrepareReport(GetFoids());
            return File(abytes, "application/pdf");
        }

        public List<Foid> GetFoids()
        {
            List<Foid> foidsList = new List<Foid>();
            // get foids to print

            Foid foid = new Foid();

            foid.Id = 1;
            foid.Name = "testFoid";
            foid.Status = 2;
            foidsList.Add(foid);


            return foidsList;

        }
    }
}