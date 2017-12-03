using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Eindwerk2018.Models
{
    public class Kabel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        //company
        [Required]
        [StringLength(200)]
        [Display(Name = "Referentie")]
        public string Reference { get; set; }

        private DateTime creatieDatum;

        public DateTime CreatieDatum
        {
            get { return creatieDatum; }
            set { creatieDatum = DateTime.Now; }
        }

    }
}