using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class FiberFoid : Fiber
    {
        [Display(Name = "fiberFoidKabelId", ResourceType = typeof(Resources.Resource))]
        public int KabelId { get; set; }

        [Display(Name = "fiberFoidKabelName", ResourceType = typeof(Resources.Resource))]
        public string KabelName { get; set; }

        [Display(Name = "fiberFoidSectieNr", ResourceType = typeof(Resources.Resource))]
        public int SectieNr { get; set; }

        [Display(Name = "fiberFoidSectieLength", ResourceType = typeof(Resources.Resource))]
        public int SectieLength { get; set; }

        [Display(Name = "fiberFoidOdfStartId", ResourceType = typeof(Resources.Resource))]
        public int OdfStartId { get; set; }

        [Display(Name = "fiberFoidOdfStartName", ResourceType = typeof(Resources.Resource))]
        public string OdfStartName { get; set; }

        [Display(Name = "fiberFoidOdfEndId", ResourceType = typeof(Resources.Resource))]
        public int OdfEndId { get; set; }

        [Display(Name = "fiberFoidOdfEndName", ResourceType = typeof(Resources.Resource))]
        public string OdfEndName { get; set; }
    }
}