using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Eindwerk2018.Models
{
    public class SectieType
    {
        public int Id { get; set; }

        public string naam { get; set; }

        public string Beschrijving { get; set; }

        public bool virtueel { get; set; }

       
    }
}