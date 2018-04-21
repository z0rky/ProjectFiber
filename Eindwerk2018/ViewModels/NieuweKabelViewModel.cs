using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class NieuweKabelViewModel
    {
        public Kabel Kabel { get; set; }

        public IEnumerable<KabelType> KabelTypes { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}