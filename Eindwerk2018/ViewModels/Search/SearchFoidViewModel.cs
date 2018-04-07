using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class SearchFoidViewModel
    {
        //public Foid Foid { get; set; }
        [Required]
        public String SearchString { get; set; }
    }
}