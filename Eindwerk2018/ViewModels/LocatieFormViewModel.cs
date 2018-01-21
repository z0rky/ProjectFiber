using System;
using System.Collections.Generic;
using Eindwerk2018.Models;


namespace Eindwerk2018.ViewModels
{
    public class LocatieFormViewModel
    {

        public IEnumerable<LocatieType> LocatieTypes { get; set; }


        public Locatie Locatie {get;set;}

    }
}
