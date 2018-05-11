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
        [Display(Name = "Foid")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(200)]
        [Display(Name = "FoidName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "FoidCreationDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatieDatum { get; set; }

        //[Required] // because with create it is not yet filled in
        [Display(Name = "FoidStatus", ResourceType = typeof(Resources.Resource))]
        public int Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "FoidLastStatusDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastStatusDate { get; set; }

        [Display(Name = "FoidRequestorId", ResourceType = typeof(Resources.Resource))]
        public int RequestorId { get; set; }
        public User Requestor { get; set; }

        [Display(Name = "FoidComments", ResourceType = typeof(Resources.Resource))]
        public string Comments{ get; set; }

        [Display(Name = "FoidLength", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int LengthCalculated { get; set; }

        [Display(Name = "FoidLengthOtdr", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")] //nope doesnt work
        public int? LengthOtdr { get; set; }

        [Display(Name = "FoidStartOdfId", ResourceType = typeof(Resources.Resource))]
        public int StartOdfId { get; set; }

        [Display(Name = "fiberFoidOdfStartName", ResourceType = typeof(Resources.Resource))]
        public String StartOdfName { get; set; }

        [Display(Name = "FoidEndOdfId", ResourceType = typeof(Resources.Resource))]
        public int EndOdfId { get; set; }

        [Display(Name = "fiberFoidOdfEndName", ResourceType = typeof(Resources.Resource))]
        public String EndOdfName { get; set; }

        [Display(Name = "FoidFibers", ResourceType = typeof(Resources.Resource))]
        public List<FiberFoid> Fibers { get; set; }

        //should replace List<FiberFoid> Fibers
        [Display(Name = "FoidFibers", ResourceType = typeof(Resources.Resource))] //for now same description
        public List<Sectie> Secties { get; set; }
    }
}