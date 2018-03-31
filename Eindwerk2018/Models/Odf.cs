﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Eindwerk2018.Models
{
    public class Odf
    {
        public int Id { get; set; }

        [Display(Name = "OdfLocationName", ResourceType = typeof(Resources.Resource))]
        public Locatie Location { get; set; }

        [Display(Name = "OdfTypeId", ResourceType = typeof(Resources.Resource))]
        public OdfType OdfType { get; set; }

        [Display(Name = "OdfName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }
    }
}
