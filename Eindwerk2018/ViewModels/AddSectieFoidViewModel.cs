using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class AddSectieFoidViewModel
    {
        public Foid Foid { get; set; }

        //First step selecting the FOID
        public List<Odf> StartOdfs{ get; set; }
        public List<Odf> EndOdfs { get; set; }
        public List<Sectie> Secties { get; set; }

        //Second step, adding the fiber
        public List<FiberFoid> Fibers { get; set; }

        //new secties
        public List<int> Newsecties { get; set; }
    }
}