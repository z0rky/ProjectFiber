using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class LocatieType
    {
        public int Id { get; set; }

        [StringLength(45)]
        [Display(Name = "LocationTypeNameNl", ResourceType = typeof(Resources.Resource))]
        public string NaamNL { get; set; }

        [StringLength(45)]
        [Display(Name = "LocationTypeNameFR", ResourceType = typeof(Resources.Resource))]
        public string NaamFR { get; set; }

        [StringLength(250)]
        [Display(Name = "LocationTypeDescNl", ResourceType = typeof(Resources.Resource))]
        public string DescNL { get; set; }

        [StringLength(250)]
        [Display(Name = "LocationTypeDescFr", ResourceType = typeof(Resources.Resource))]
        public string DescFR { get; set; }

        //public LocatieType(int id, string nl, string fr, string descnl, string descfr)
        //{
        //    Id = id;
        //    NaamNL = nl;
        //    NaamFR = fr;
        //    DescNL = descnl;
        //    DescFR = descfr;
        //}
    }
}
