using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class Sectie
    {
       
        public int Id { get; set; }

        [Display(Name = "SectieNr", ResourceType = typeof(Resources.Resource))]
        public int SectieNr { get; set; }

        [Display(Name = "SectieTypeId", ResourceType = typeof(Resources.Resource))]
        public int SectionTypeId { get; set; }

        [Display(Name = "SectieTypeName", ResourceType = typeof(Resources.Resource))]
        public string SectionTypeName { get; set; }

        [Display(Name = "SectieKabelId", ResourceType = typeof(Resources.Resource))]
        public int KabelId { get; set; }

        [Display(Name = "SectieKabelName", ResourceType = typeof(Resources.Resource))]
        public string KabelName { get; set; }

        [Display(Name = "SectieOdfStartId", ResourceType = typeof(Resources.Resource))]
        public int OdfStartId { get; set; }

        [Display(Name = "SectieOdfStartName", ResourceType = typeof(Resources.Resource))]
        public string OdfStartName { get; set; }

        [Display(Name = "SectieOdfEndId", ResourceType = typeof(Resources.Resource))]
        public int OdfEndId { get; set; }

        [Display(Name = "SectieOdfEndName", ResourceType = typeof(Resources.Resource))]
        public string OdfEndName { get; set; }

        [Display(Name = "SectieLength", ResourceType = typeof(Resources.Resource))]
        public int Lengte { get; set; }

        [Display(Name = "SectieActive", ResourceType = typeof(Resources.Resource))]
        public bool Active { get; set; }

        public List<Fiber> Fibers { get; set; }
    }
}