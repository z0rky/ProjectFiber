using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class EditFiberSectieViewModel
    {
        public Foid Foid { get; set; }

        public int NrOfFibers { get; set; }
        public int OldNrOfFibers { get; set; } //to check on return
    }
}