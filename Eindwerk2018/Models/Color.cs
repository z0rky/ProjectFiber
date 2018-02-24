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

        [Display(Name = "ColorNameEn", ResourceType = typeof(Resources.Resource))]
        public String NameEn { get; set; }

        [Display(Name = "ColorNameNL", ResourceType = typeof(Resources.Resource))]
        public String NameNl { get; set; }

        [Display(Name = "ColorNameFr", ResourceType = typeof(Resources.Resource))]
        public String NameFr { get; set; }
    }
}