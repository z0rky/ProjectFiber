using System;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "fiberQuality", ResourceType = typeof(Resources.Resource))]
        public string Quality { get; set; }

        [Display(Name = "fiberFoid", ResourceType = typeof(Resources.Resource))]
        public int Foid { get; set; }

        [Display(Name = "fiberFoidName", ResourceType = typeof(Resources.Resource))]
        public String FoidName { get; set; }

        [Display(Name = "fiberFoidSerialNr", ResourceType = typeof(Resources.Resource))]
        public int FoidSerialNr { get; set; }

        [Display(Name = "fiberFoidFiberNr", ResourceType = typeof(Resources.Resource))]
        public int FoidFibreNr { get; set; }

        [Display(Name = "fiberActive", ResourceType = typeof(Resources.Resource))]
        public Boolean Active { get; set; }
    }
}