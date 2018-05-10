using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class LocatieType
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(45)]
        [Display(Name = "LocationTypeNameNl", ResourceType = typeof(Resources.Resource))]
        public string NaamNL { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(45)]
        [Display(Name = "LocationTypeNameFR", ResourceType = typeof(Resources.Resource))]
        public string NaamFR { get; set; }

        [StringLength(250)]
        [Display(Name = "LocationTypeDescNl", ResourceType = typeof(Resources.Resource))]
        public string DescNL { get; set; }

        [StringLength(250)]
        [Display(Name = "LocationTypeDescFr", ResourceType = typeof(Resources.Resource))]
        public string DescFR { get; set; }
    }
}
