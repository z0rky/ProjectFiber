using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class SearchKabelResultViewModel
    {
        public IEnumerable<Kabel> GezochteKabels { get; set; }
    }
}