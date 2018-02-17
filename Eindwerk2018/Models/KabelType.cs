using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class KabelType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "ColorNameNl", ResourceType = typeof(Resources.Resource))] //used from color, but it is the same
        public string NameNL { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "ColorNameFr", ResourceType = typeof(Resources.Resource))]
        public string NameFR { get; set; }
    }
}