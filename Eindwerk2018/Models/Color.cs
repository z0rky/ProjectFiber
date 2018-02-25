using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ColorNameEn", ResourceType = typeof(Resources.Resource))]
        public String NameEn { get; set; }

        [Required]
        [Display(Name = "ColorNameNl", ResourceType = typeof(Resources.Resource))]
        public String NameNl { get; set; }

        [Required]
        [Display(Name = "ColorNameFr", ResourceType = typeof(Resources.Resource))]
        public String NameFr { get; set; }
    }
}