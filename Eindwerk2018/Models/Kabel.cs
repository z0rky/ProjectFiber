using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Eindwerk2018.Models
{
    public class Kabel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(200)]
        [Display(Name = "KabelName", ResourceType = typeof(Resources.Resource))]
        public string Naam { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "KabelKabelType", ResourceType = typeof(Resources.Resource))]
        public KabelType KabelType { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "KabelOwner", ResourceType = typeof(Resources.Resource))]
        public Company Owner { get; set; }

        //company
        [StringLength(200)]
        [Display(Name = "KabelReference", ResourceType = typeof(Resources.Resource))]
        public string Reference { get; set; }

        [Display(Name = "KabelCreationDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatieDatum { get; set; }
    }
}
