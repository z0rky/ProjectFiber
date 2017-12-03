using System;
using System.Collections.Generic;
using Eindwerk2018.Models;


namespace Eindwerk2018.ViewModels
{
    public class NieuweLocatieViewModel
    {

        public IEnumerable<LocatieType> LocatieTypes { get; set; }


        public LocatieModel Locatie {get;set;}

    }
}
