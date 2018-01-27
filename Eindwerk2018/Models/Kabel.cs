using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Eindwerk2018.Models
{
    public class Kabel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "KabelName", ResourceType = typeof(Resources.Resource))]
        public string Naam { get; set; }

        //company
        [Required]
        [StringLength(200)]
        [Display(Name = "KabelReference", ResourceType = typeof(Resources.Resource))]
        public string Reference { get; set; }

        private DateTime creatieDatum;

        [Display(Name = "KabelCreationDate", ResourceType = typeof(Resources.Resource))]
        public DateTime CreatieDatum
        {
            get { return creatieDatum; }
            set { creatieDatum = DateTime.Now; }
        }
    }
}
