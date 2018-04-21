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

        [Required]
        [StringLength(200)]
        [Display(Name = "KabelName", ResourceType = typeof(Resources.Resource))]
        public string Naam { get; set; }

        [Required]
        [Display(Name = "KabelKabelType", ResourceType = typeof(Resources.Resource))]
        public int KabelTypeId { get; set; }

        [Required]
        [Display(Name = "KabelOwner", ResourceType = typeof(Resources.Resource))]
        public int OwnerId { get; set; }

        //company
        [StringLength(200)]
        [Display(Name = "KabelReference", ResourceType = typeof(Resources.Resource))]
        public string Reference { get; set; }

        [Display(Name = "KabelCreationDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatieDatum { get; set; }
    }
}
