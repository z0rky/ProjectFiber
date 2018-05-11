using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class NieuweOdfViewModel
    {
        //probleem met dit is dat alles van odf.locatie en alles van odftypes moet ingevuld zijn, ander is het niet vallid
        //public Odf Odf { get; set; }

        //oplossing enkel required values
        public int Id { get; set; }  //for edit

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "OdfLocationName", ResourceType = typeof(Resources.Resource))]
        public String LocatieName { get; set; }

        [Required]
        public int LocatieId { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "OdfTypeId", ResourceType = typeof(Resources.Resource))]
        public int OdfTypeId { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "OdfName", ResourceType = typeof(Resources.Resource))]
        public String OdfName { get; set; }

        public IEnumerable<OdfType> OdfTypes { get; set; }
    }
}