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
            PdfReports pdfReportsFoid = new PdfReports();
            byte[] abytes = pdfReportsFoid.PrepareReport(GetFoids());
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
        
        //public ActionResult ReportSectie(Sectie sectie)
        //{
        //    PdfReportsSectie pdfReportsSectie = new PdfReportsSectie();
        //    byte[] abytes2 = pdfReportsSectie.PrepareReport(GetSectie());
        //    return File(abytes2, "application/pdf");
        //}

        //public List<Sectie> GetSectie()
        //{
        //    List<Sectie> sectieList = new List<Sectie>();
        //    //// get foids to print

        //    Sectie sectie = new Sectie();

        //    sectie.Id = 1;
        //    sectie.Lengte = 23;
        //    sectieList.Add(sectie);


        //    return sectieList;

        //}
    }
}