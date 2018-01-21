using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class Adres
    {
        public int Id { get; set; }

        [Display(Name = "AdresStraat", ResourceType = typeof(Resources.Resource))]
        public string Straat { get; set; }

        [Display(Name = "AdresNummer", ResourceType = typeof(Resources.Resource))]
        public int Nummer { get; set; }

        [Display(Name = "AdresPostcode", ResourceType = typeof(Resources.Resource))]
        public int PostCode { get; set; }

        [Display(Name = "AdresGemeente", ResourceType = typeof(Resources.Resource))]
        public string Gemeente { get; set; }
    }
}