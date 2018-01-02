using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class Adres
    {
        public int Id { get; set; }

        public string Straat { get; set; }

        public int Nummer { get; set; }

        public int PostCode { get; set; }

        public string Gemeente { get; set; }
    }
}