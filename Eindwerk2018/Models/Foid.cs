using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Eindwerk2018.Models
{
    public class Foid
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Required]
        public bool Status { get; set; }

        public int RequestorId { get; set; }

        private DateTime creatieDatum;

        public DateTime CreatieDatum
        {
            get { return creatieDatum; }
            set { creatieDatum = DateTime.Now; }
        }





    }
}