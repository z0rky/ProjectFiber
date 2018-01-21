using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class Sectie
    {
       
        public int Id { get; set; }

        public int SectionTypeId { get; set; }

        public int KabelId { get; set; }

        public int OdfStartId { get; set; }

        public int OdfEndId { get; set; }

        public int SectieTypeId { get; set; }

        public int Lengte { get; set; }

        public bool Active { get; set; }





    }
}