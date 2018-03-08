using Eindwerk2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eindwerk2018.ViewModels
{
    public class NieuweSectieViewModel
    {
        public Sectie Sectie { get; set; }

        public IEnumerable<SectieType> SectieTypes { get; set; }
    }
}