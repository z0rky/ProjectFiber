using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Eindwerk2018.Models;


namespace Eindwerk2018.ViewModels
{
    public class SearchLocatieResultViewModel
    {
        public IEnumerable<Locatie> GezochteLocaties { get; set; }



    }
}