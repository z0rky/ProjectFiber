using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class SectieType
    {
        public int Id { get; set; }

        [Display(Name = "SectieTypeName", ResourceType = typeof(Resources.Resource))]
        public string Naam { get; set; }

        [Display(Name = "SectieTypeDescription", ResourceType = typeof(Resources.Resource))]
        public string Beschrijving { get; set; }

        [Display(Name = "SectieTypeVirtual", ResourceType = typeof(Resources.Resource))]
        public bool Virtueel { get; set; }

        [Display(Name = "SectieFibers", ResourceType = typeof(Resources.Resource))]
        List <Fiber> Fibers { get; set; }
    }
}