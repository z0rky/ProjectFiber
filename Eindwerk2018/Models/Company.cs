using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Eindwerk2018.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "CompanyName", ResourceType = typeof(Resources.Resource))]
        public String Name { get; set; }
    }
}