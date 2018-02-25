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

        [DataType(DataType.Date)]
        [Display(Name = "FoidCreationDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatieDatum { get; set; }

        [Required]
        [Display(Name = "FoidStatus", ResourceType = typeof(Resources.Resource))]
        public int Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "FoidLastStatusDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastStatusDate { get; set; }

        [Display(Name = "FoidRequestorId", ResourceType = typeof(Resources.Resource))]
        public int RequestorId { get; set; }

        [Display(Name = "FoidComments", ResourceType = typeof(Resources.Resource))]
        public string Comments{ get; set; }

        [Display(Name = "FoidLength", ResourceType = typeof(Resources.Resource))]
        public int LengthCalculated { get; set; }

        [Display(Name = "FoidLengthOtdr", ResourceType = typeof(Resources.Resource))]
        public int LengthOtdr { get; set; }

        [Display(Name = "FoidStartOdfId", ResourceType = typeof(Resources.Resource))]
        public int StartOdfId { get; set; }

        [Display(Name = "FoidEndOdfId", ResourceType = typeof(Resources.Resource))]
        public int EndOdfId { get; set; }

        [Display(Name = "FoidFibers", ResourceType = typeof(Resources.Resource))]
        public List<FiberFoid> Fibers { get; set; }
    }
}