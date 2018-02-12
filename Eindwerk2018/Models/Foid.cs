using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class Foid
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "FoidName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "FoidStatus", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        [Display(Name = "FoidRequestorId", ResourceType = typeof(Resources.Resource))]
        public int RequestorId { get; set; }

        private DateTime creatieDatum;

        [Display(Name = "FoidCreationDate", ResourceType = typeof(Resources.Resource))]
        public DateTime CreatieDatum
        {
            get { return creatieDatum; }
            set { creatieDatum = DateTime.Now; }
        }
    }
}