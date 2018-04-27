using Eindwerk2018.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.ViewModels
{
    public class SplitSectieViewModel
    {
        public Sectie Sectie { get; set; }

        [Display(Name = "SectieSplitOdf", ResourceType = typeof(Resources.Resource))]
        public int SplitOdfId { get; set; }
    }
}