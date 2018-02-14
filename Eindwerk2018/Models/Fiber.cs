using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class Fiber
    {
        [Display(Name = "fiberOrderNr", ResourceType = typeof(Resources.Resource))]
        public int OrderNr { get; set; }

        [Display(Name = "fiberFiberNr", ResourceType = typeof(Resources.Resource))]
        public int FiberNr { get; set; }

        [Display(Name = "fiberFiberColor", ResourceType = typeof(Resources.Resource))]
        public Color FiberColor { get; set; }

        [Display(Name = "fiberModuleNr", ResourceType = typeof(Resources.Resource))]
        public int ModuleNr { get; set; }

        [Display(Name = "fiberModuleColor", ResourceType = typeof(Resources.Resource))]
        public Color ModuleColor { get; set; }
    }
}