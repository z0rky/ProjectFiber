using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Eindwerk2018.Models
{
    public class Odf
    {
        public int Id { get; set; }
        [Display(Name = "OdfLocationId", ResourceType = typeof(Resources.Resource))]
        public int Location_id { get; set; }
        [Display(Name = "OdfTypeId", ResourceType = typeof(Resources.Resource))]
        public int Type_id { get; set; }
        [Display(Name = "OdfName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        public Odf(int id, int location_id, int type_id, string name)
        {
            this.Id = id;
            this.Location_id = location_id;
            this.Type_id = type_id;
            this.Name = name;
        }
    }
}
