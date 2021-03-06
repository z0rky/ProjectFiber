﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Eindwerk2018.Models
{
    public class OdfType
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "OdfTypeName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "OdfTypeDescription", ResourceType = typeof(Resources.Resource))]
        public string Description { get; set; }
    }
}
